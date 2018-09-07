using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Classic.World.Authentication
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 5)]
    internal struct S_ServerAuthenticationResponse_Success
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] len; // should be ushort. byte[] since it needs to be big endian
        public ushort cmd; 
        public byte result;
        //public uint unk1;
        //public byte unk2;
        //public uint unk3; // maybe uint
    }

    public class ServerAuthenticationResponse
    {
        public byte[] Get(/* TODO */)
        {
            byte[] temp = BitConverter.GetBytes((ushort)11);
            Array.Reverse(temp); // Converting temp to big endian.

            var bytes = new List<byte>();
            bytes.AddRange(temp);
            bytes.Add(0x0C);
            bytes.Add(0x30);
            bytes.Add(0x78);
            bytes.Add(0x00);
            bytes.Add(0x00);
            bytes.Add(0x00);
            bytes.Add(0x00);
            bytes.Add(0x00);
            bytes.Add(0x00);
            bytes.Add(0x00);
            bytes.Add(0x00);

            return bytes.ToArray();
        }
    }
}
