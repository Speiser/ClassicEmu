using System.Text;
using Classic.Shared;
using Classic.World.Cryptography;

namespace Classic.World.Packets.Client
{
    public class CMSG_UPDATE_ACCOUNT_DATA
    {
        public CMSG_UPDATE_ACCOUNT_DATA(byte[] data)
        {
            using var reader = new PacketReader(data);
            this.Type = reader.ReadUInt32();
            this.UncompressedSize = reader.ReadUInt32();
            var rest = reader.ReadBytes(data.Length - 8);

            if (this.UncompressedSize == 0)
            {
                this.Data = Encoding.ASCII.GetString(rest);
                return;
            }

            var uncompressed = Compression.Uncompress(rest);
            this.Data = Encoding.ASCII.GetString(uncompressed);
        }

        public uint Type { get; }
        public uint UncompressedSize { get; }
        public string Data { get; }
    }
}
