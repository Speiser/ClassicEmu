using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Classic.Common
{
    public class PacketReader : BinaryReader
    {
        public PacketReader(byte[] packet) : base(new MemoryStream(packet)) { }

        public void Skip(int amount) => this.ReadBytes(amount);

        public ushort ReadUInt16Reverse()
        {
            var bytes = this.ReadBytes(2);
            return BitConverter.ToUInt16(new [] { bytes[1], bytes[0] });
        }

        public override string ReadString()
        {
            var account = new List<byte>();

            while (this.PeekChar() != 0)
            {
                account.Add(this.ReadByte());
            }

            this.ReadByte(); // skip the "\0"
            return Encoding.ASCII.GetString(account.ToArray());
        }
    }
}
