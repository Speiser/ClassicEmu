using Classic.Common;
using static Classic.World.Opcode;

namespace Classic.World.Authentication
{
    public class ServerAuthenticationChallenge
    {
        public static byte[] AuthSeed => new byte[] { 0x33, 0x18, 0x34, 0xC8 };

        public static byte[] Create()
        {
            using (var packet = new PacketWriter())
            {
                return packet
                    .WriteUInt16Reversed(6) // length
                    .WriteUInt16((ushort)SMSG_AUTH_CHALLENGE)
                    .WriteBytes(AuthSeed)
                    .Build();
            }
        }
    }
}
