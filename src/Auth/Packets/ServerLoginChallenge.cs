using Classic.Auth.Cryptography;
using Classic.Auth.Cryptography.Extensions;
using Classic.Auth.Packets.Enums;
using Classic.Shared;

namespace Classic.Auth.Packets
{
    public class ServerLoginChallenge
    {
        public static byte[] Success(SecureRemotePasswordProtocol srp) => new PacketWriter()
            .WriteUInt8(/* cmd   */ (byte)Opcode.LoginChallenge)
            .WriteUInt8(/* error */ (byte)AuthenticationStatus.Success)
            .WriteUInt8(/* unk2  */ 0)
            .WriteBytes(/* B[32] */ srp.B)
            .WriteUInt8(/* g_len */ 1)
            .WriteUInt8(/* g[1]  */ SecureRemotePasswordProtocol.g)
            .WriteUInt8(/* N_len */ 32)
            .WriteBytes(/* N[32] */ srp.N.ToProperByteArray())
            .WriteBytes(/* s     */ srp.s)
            .WriteBytes(/* unk3  */
                0x2A, 0xD5, 0x48, 0xCC, 0x9B, 0x9D, 0xA1, 0x99,
                0xCC, 0x04, 0x7A, 0x60, 0x91, 0x15, 0x6C, 0x51)
            .WriteUInt8(/* unk4  */ 0)
            .Build();
    }
}