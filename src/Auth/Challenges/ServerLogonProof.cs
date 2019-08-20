using Classic.Common;
using Classic.Cryptography;
using static Classic.Auth.Opcode;

namespace Classic.Auth.Challenges
{
    public class ServerLogonProof
    {
        private readonly SecureRemotePasswordProtocol srp;

        public ServerLogonProof(SecureRemotePasswordProtocol srp)
        {
            this.srp = srp;
        }

        public byte[] Get(byte[] clientPublicValue, byte[] clientProof)
        {
            if (this.srp.ValidateClientProof(clientPublicValue, clientProof))
            {
                using (var packet = new PacketWriter())
                {
                    return packet
                        .WriteUInt8( /* cmd   */ (byte) LOGIN_PROOF)
                        .WriteUInt8( /* error */ 0)
                        .WriteBytes( /* M[20] */ this.srp.M)
                        .WriteUInt32(/* unk   */ 0)
                        .Build();
                }
            }

            // Failed
            return new byte[] { 0, 0, 4 };
        }
    }
}