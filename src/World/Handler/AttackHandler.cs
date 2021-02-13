using System.Threading.Tasks;
using Classic.World.Messages.Client;
using Classic.World.Messages.Server;

namespace Classic.World.Handler
{
    public class AttackHandler
    {
        [OpcodeHandler(Opcode.CMSG_SET_SELECTION)]
        public static Task OnSetSelection(WorldClient client, byte[] data)
        {
            var request = new CMSG_SET_SELECTION(data);
            client.Player.TargetId = request.TargetId;
            return Task.CompletedTask;
        }

        [OpcodeHandler(Opcode.CMSG_CAST_SPELL)]
        public static async Task OnCastSpell(WorldClient client, byte[] data)
        {
            var request = new CMSG_CAST_SPELL(data);
            client.Log($"Casting: {request.SpellId}");
            await client.SendPacket(SMSG_CAST_RESULT.Success(request.SpellId));
        }
    }
}
