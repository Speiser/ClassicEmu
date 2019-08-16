using Classic.World.Messages;

namespace Classic.World.Handler
{
    public class UtilHandler
    {
        [OpcodeHandler(Opcode.CMSG_PING)]
        public static void OnPing(WorldClient client, byte[] data)
        {
            client.SendPacket(new SMSG_PONG(data));
        }
    }
}
