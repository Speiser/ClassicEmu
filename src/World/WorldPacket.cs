using System.Linq;

namespace Classic.World
{
    public class WorldPacket
    {
        public WorldPacket(byte[] header, byte[] data)
        {
            this.Header = header;
            this.Data = data;
        }

        public byte[] Header { get; }
        public byte[] Data { get; }

        public byte[] ToByteArray() => this.Header.Concat(this.Data).ToArray();
    }
}
