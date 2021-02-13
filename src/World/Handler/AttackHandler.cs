using System.Threading.Tasks;
using Classic.World.Messages.Client;
using Classic.World.Messages.Server;

namespace Classic.World.Handler
{
    public class AttackHandler
    {
        [OpcodeHandler(Opcode.CMSG_SET_SELECTION)]
        public static Task OnSetSelection(HandlerArguments args)
        {
            var request = new CMSG_SET_SELECTION(args.Data);
            args.Client.Player.TargetId = request.TargetId;
            return Task.CompletedTask;
        }

        [OpcodeHandler(Opcode.CMSG_CAST_SPELL)]
        public static async Task OnCastSpell(HandlerArguments args)
        {
            var request = new CMSG_CAST_SPELL(args.Data);
            args.Client.Log($"Casting: {request.SpellId}");

            // ????
            await args.Client.SendPacket(SMSG_CAST_RESULT.Success(request.SpellId));

            // Opcode.SMSG_SPELLLOGEXECUTE
        }
    }
}
