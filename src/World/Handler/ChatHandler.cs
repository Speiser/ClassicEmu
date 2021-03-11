using System.Threading.Tasks;
using Classic.World.Data;
using Classic.World.Messages;
using Classic.World.Messages.Client;

namespace Classic.World.Handler
{
    public class ChatHandler
    {
        [OpcodeHandler(Opcode.CMSG_MESSAGECHAT)]
        public static async Task OnMessageChat(PacketHandlerContext c)
        {
            var request = new CMSG_MESSAGECHAT(c.Data);

            // debugging stuff :D
            if (request.Message.StartsWith(".spawn"))
            {
                var spawnId = int.Parse(request.Message.Split(" ")[1]);
                var creature = new Creature { Model = spawnId, Position = c.Client.Character.Position.Copy() };
                await c.WorldState.SpawnCreature(creature);
            }
        }
    }
}
