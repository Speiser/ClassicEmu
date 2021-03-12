using Classic.Auth.Cryptography;
using Classic.Shared;
using static Classic.Auth.Opcode;
using Classic.Shared.Data;
using Classic.Auth.Data.Enums;

namespace Classic.Auth.Challenges
{
    public class ServerLogonProof
    {
        private readonly SecureRemotePasswordProtocol srp;
        private readonly int build;

        public ServerLogonProof(SecureRemotePasswordProtocol srp, int build)
        {
            this.srp = srp;
            this.build = build;
        }

        public byte[] Get(byte[] clientPublicValue, byte[] clientProof)
        {
            if (this.srp.ValidateClientProof(clientPublicValue, clientProof))
            {
                using var packet = new PacketWriter();
                packet
                    .WriteUInt8( /* cmd   */ (byte)LOGIN_PROOF)
                    .WriteUInt8( /* error */ (byte)AuthenticationStatus.Success)
                    .WriteBytes( /* M[20] */ this.srp.M)
                    .WriteUInt32(/* unk   */ 0);

                if (this.build > ClientBuild.Vanilla)
                {
                    packet
                        .WriteUInt32(/* unk2 */ 0)
                        .WriteUInt16(/* unk3 */ 0);
                }

                return packet.Build();
            }

            // Failed
            return new byte[] { 0, 0, 4 };
        }
    }
}