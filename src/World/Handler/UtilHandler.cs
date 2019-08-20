using System.Threading.Tasks;
using Classic.World.Messages;

namespace Classic.World.Handler
{
    public class UtilHandler
    {
        [OpcodeHandler(Opcode.CMSG_PING)]
        public static async Task OnPing(WorldClient client, byte[] data) => await client.SendPacket(new SMSG_PONG(data));
    }
}
