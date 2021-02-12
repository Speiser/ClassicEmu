using Classic.Common;

namespace Classic.World.Messages.Client
{
    public class CMSG_ZONEUPDATE
    {
        public CMSG_ZONEUPDATE(byte[] data)
        {
            using var reader = new PacketReader(data);
            NewZone = reader.ReadUInt32();
        }

        public uint NewZone { get; }
    }
}
