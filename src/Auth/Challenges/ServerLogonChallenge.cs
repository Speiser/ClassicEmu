using Classic.Common;
using Classic.Cryptography;
using Classic.Cryptography.Extensions;
using static Classic.Auth.Opcode;

namespace Classic.Auth.Challenges
{
    public class ServerLogonChallenge
    {
        private readonly SecureRemotePasswordProtocol srp;

        public ServerLogonChallenge(SecureRemotePasswordProtocol srp)
        {
            this.srp = srp;
        }

        public byte[] Get()
        {
            using (var packet = new PacketWriter())
            {
                return packet
                    .WriteUInt8(/* cmd   */ (byte)LOGIN_CHALL)
                    .WriteUInt8(/* error */ 0)
                    .WriteUInt8(/* unk2  */ 0)
                    .WriteBytes(/* B[32] */ this.srp.B)
                    .WriteUInt8(/* g_len */ 1)
                    .WriteUInt8(/* g[1]  */ SecureRemotePasswordProtocol.g)
                    .WriteUInt8(/* N_len */ 32)
                    .WriteBytes(/* N[32] */ this.srp.N.ToProperByteArray())
                    .WriteBytes(/* s     */ this.srp.s)
                    .WriteBytes(/* unk3  */
                        0x2A, 0xD5, 0x48, 0xCC, 0x9B, 0x9D, 0xA1, 0x99,
                        0xCC, 0x04, 0x7A, 0x60, 0x91, 0x15, 0x6C, 0x51)
                    .WriteUInt8(/* unk4  */ 0)
                    .Build();
            }
        }
    }
}