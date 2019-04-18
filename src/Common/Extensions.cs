using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Classic.Common
{
    public static class Extensions
    {
        // https://github.com/miceiken/WoWClassicAuthServer/blob/master/WoWClassic.Common/Extensions.cs#L45
        public static byte[] ToProperByteArray(this BigInteger b)
        {
            var bytes = b.ToByteArray();
            if (b.Sign == 1 && bytes.Length > 1 && bytes[bytes.Length - 1] == 0)
                Array.Resize(ref bytes, bytes.Length - 1);
            return bytes;
        }

        // https://github.com/miceiken/WoWClassicAuthServer/blob/master/WoWClassic.Common/Extensions.cs#L54
        public static BigInteger ToPositiveBigInteger(this IEnumerable<byte> bytes)
            => new BigInteger(bytes.Concat(new byte[] { 0 }).ToArray());

        public static T AsEnum<T>(this byte b) => (T)Enum.ToObject(typeof(T), b);

        // https://github.com/andrewmunro/Vanilla/blob/f0f8ad5f833f299cf746c200dba143c530240c35/Vanilla/Vanilla.Core/Extensions/BinaryWriterExtension.cs
        public static byte[] ToPackedUInt64(this ulong number)
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            {
                var buffer = BitConverter.GetBytes(number);

                byte mask = 0;
                var startPos = writer.BaseStream.Position;

                writer.Write(mask);

                for (var i = 0; i < 8; i++)
                {
                    if (buffer[i] != 0)
                    {
                        mask |= (byte)(1 << i);
                        writer.Write(buffer[i]);
                    }
                }

                var endPos = writer.BaseStream.Position;

                writer.BaseStream.Position = startPos;
                writer.Write(mask);
                writer.BaseStream.Position = endPos;

                return ms.ToArray();
            }
        }
    }
}
