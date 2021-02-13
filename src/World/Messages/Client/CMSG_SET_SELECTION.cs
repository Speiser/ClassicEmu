using Classic.Common;

namespace Classic.World.Messages.Client
{
    public class CMSG_SET_SELECTION
    {
        public CMSG_SET_SELECTION(byte[] data)
        {
            using var reader = new PacketReader(data);
            TargetId = reader.ReadUInt64();
        }

        public ulong TargetId { get; }
    }
}
