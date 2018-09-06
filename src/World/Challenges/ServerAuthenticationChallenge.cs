using Classic.Common;
using System;
using System.Runtime.InteropServices;

namespace Classic.World.Challenges
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 8)]
    internal struct S_ServerAuthenticationChallenge
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] len; // should be ushort. byte[] since it needs to be big endian
        public ushort cmd;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] auth_seed;
    }

    public class ServerAuthenticationChallenge
    {
        public byte[] Get()
        {
            byte[] temp = BitConverter.GetBytes((ushort)6);
            Array.Reverse(temp); // Converting temp to big endian.

            return ByteSerializer.Serialize(new S_ServerAuthenticationChallenge
            {
                len = temp,
                cmd = 492,
                auth_seed = new byte[] { 0x33, 0x18, 0x34, 0xC8 },
            });
        }
    }
}
