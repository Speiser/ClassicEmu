using Classic.Shared;

namespace Classic.World.Messages.Client
{
    public class CMSG_ATTACKSWING
    {
        public CMSG_ATTACKSWING(byte[] data)
        {
            using var reader = new PacketReader(data);
            this.Guid = reader.ReadUInt64();
        }

        public ulong Guid { get; }
    }
}
