using System.Runtime.InteropServices;
using Classic.Common;
using Classic.Cryptography;
using static Classic.Auth.Opcode;

namespace Classic.Auth.Challenges
{

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 26)]
    internal struct S_ServerLogonProof
    {
        public byte cmd;
        public byte error;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public byte[] M;

        public byte unk1;
        public byte unk2;
        public byte unk3;
        public byte unk4;
    }

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
                var response = new S_ServerLogonProof
                {
                    cmd = (byte)LOGIN_PROOF,
                    error = 0,
                    M = this.srp.M,
                    unk1 = 0,
                    unk2 = 0,
                    unk3 = 0,
                    unk4 = 0
                };

                return ByteSerializer.Serialize(response);
            }
            else
            {
                return new byte[] { 0, 0, 4 };
            }
        }
    }
}