using System;
using System.Runtime.InteropServices;

namespace Classic.Common
{
    public class ByteSerializer
    {
        public static byte[] Serialize<T>(T item)
        {
            int rawSize = Marshal.SizeOf(typeof(T));
            IntPtr buffer = Marshal.AllocHGlobal(rawSize);
            Marshal.StructureToPtr(item, buffer, false);
            byte[] rawData = new byte[rawSize];
            Marshal.Copy(buffer, rawData, 0, rawSize);
            Marshal.FreeHGlobal(buffer);
            return rawData;
        }
    }
}
