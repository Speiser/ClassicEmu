using System;
using System.IO;
using System.Text;

namespace Classic.Common
{
    public class PacketWriter : IDisposable
    {
        private readonly MemoryStream stream;
        private readonly BinaryWriter writer;

        public PacketWriter()
        {
            this.stream = new MemoryStream();
            this.writer = new BinaryWriter(this.stream);
        }

        public PacketWriter WriteUInt8(byte value)
        {
            this.writer.Write(value);
            return this;
        }

        public PacketWriter WriteUInt16(ushort value)
        {
            this.writer.Write(value);
            return this;
        }

        public PacketWriter WriteUInt16Reversed(ushort value)
        {
            byte[] temp = BitConverter.GetBytes(value);
            Array.Reverse(temp);
            return this.WriteBytes(temp);
        }

        public PacketWriter WriteUInt32(uint value)
        {
            this.writer.Write(value);
            return this;
        }

        public PacketWriter WriteUInt64(ulong value)
        {
            this.writer.Write(value);
            return this;
        }

        public PacketWriter WriteBytes(params byte[] values)
        {
            this.writer.Write(values);
            return this;
        }

        public PacketWriter WriteByteArrayWithLength(byte[] value, int length)
        {
            this.writer.Write(value, 0, length);
            return this;
        }

        public PacketWriter WriteInt32(int value)
        {
            this.writer.Write(value);
            return this;
        }

        public PacketWriter WriteFloat(float value)
        {
            this.writer.Write(value);
            return this;
        }

        public PacketWriter WriteString(string value, bool nullTerminated = true)
        {
            if (nullTerminated) value += "\0";
            this.WriteBytes(Encoding.ASCII.GetBytes(value));
            return this;
        }

        public byte[] Build() => this.stream.ToArray();

        public void Dispose()
        {
            this.writer?.Dispose();
            this.stream?.Dispose();
        }
    }
}
