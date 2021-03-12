using Classic.Shared;

namespace Classic.World.Messages.Client
{
    public class CMSG_PING
    {
        public CMSG_PING(byte[] data)
        {
            using (var reader = new PacketReader(data))
            {
                Ping = reader.ReadUInt32();
                Latency = reader.ReadUInt32();
            }
        }

        public uint Ping { get; }
        public uint Latency { get; }
    }
}
