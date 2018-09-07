using System.Linq;
using System.Runtime.InteropServices;
using Classic.Common;
using Classic.Cryptography;
using static Classic.Auth.Opcode;

namespace Classic.Auth.Challenges
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 119)]
    internal struct S_ServerLogonChallenge
    {
        public byte cmd;
        public byte error;
        public byte unk2;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] B;
        public byte g_len;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public byte[] g;
        public byte N_len;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] N;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] s;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] unk3;
        public byte unk4;
    }

    public class ServerLogonChallenge
    {
        private readonly SecureRemotePasswordProtocol srp;

        public ServerLogonChallenge(SecureRemotePasswordProtocol srp)
        {
            this.srp = srp;
        }

        public byte[] Get()
        {
            var response = new S_ServerLogonChallenge()
            {
                cmd = (byte)LOGIN_CHALL,
                error = 0,
                unk2 = 0,
                B = this.srp.B,
                g_len = 1,
                g = new byte[] { SecureRemotePasswordProtocol.g },
                N_len = 32,
                N = this.srp.N.ToByteArray().Take(32).ToArray(),
                s = this.srp.s,
                unk3 = new byte[] {
                    0x2A, 0xD5, 0x48, 0xCC, 0x9B, 0x9D, 0xA1, 0x99,
                    0xCC, 0x04, 0x7A, 0x60, 0x91, 0x15, 0x6C, 0x51
                },
                unk4 = 0
            };

            return ByteSerializer.Serialize(response);
        }
    }
}