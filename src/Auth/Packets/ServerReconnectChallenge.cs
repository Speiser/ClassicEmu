using Classic.Auth.Data.Enums;
using Classic.Shared;

namespace Classic.Auth.Packets
{
    public class ServerReconnectChallenge
    {
        public static byte[] Success() => new PacketWriter()
            .WriteUInt8((byte)Opcode.ReconnectChallenge)
            .WriteUInt8((byte)AuthenticationStatus.Success)
            .WriteBytes(/* challenge_data  */
                0x2A, 0xD5, 0x48, 0xCC, 0x9B, 0x9D, 0xA1, 0x99,
                0xCC, 0x04, 0x7A, 0x60, 0x91, 0x15, 0x6C, 0x51)
            .WriteUInt64(0) // unk1
            .WriteUInt64(0) // unk2
            .Build();
    }
}