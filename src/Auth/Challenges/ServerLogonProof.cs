using Classic.Auth.Entities;
using Classic.Auth.Cryptography;
using Classic.Shared;
using static Classic.Auth.Opcode;

namespace Classic.Auth.Challenges
{
    public class ServerLogonProof
    {
        private readonly SecureRemotePasswordProtocol srp;
        private readonly GameVersion gameVersion;

        public ServerLogonProof(SecureRemotePasswordProtocol srp, GameVersion gameVersion)
        {
            this.srp = srp;
            this.gameVersion = gameVersion;
        }

        public byte[] Get(byte[] clientPublicValue, byte[] clientProof)
        {
            if (this.srp.ValidateClientProof(clientPublicValue, clientProof))
            {
                using var packet = new PacketWriter();
                packet
                    .WriteUInt8( /* cmd   */ (byte)LOGIN_PROOF)
                    .WriteUInt8( /* error */ 0)
                    .WriteBytes( /* M[20] */ this.srp.M)
                    .WriteUInt32(/* unk   */ 0);

                if (this.gameVersion == GameVersion.WotLK)
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