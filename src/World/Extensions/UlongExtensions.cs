using System;
using System.IO;

namespace Classic.World.Extensions
{
    internal static class UlongExtensions
    {
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
