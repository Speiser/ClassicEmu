using Classic.Shared;
using Classic.World.Data.Enums.Map;

namespace Classic.World.Messages.Client
{
    public class CMSG_ZONEUPDATE
    {
        public CMSG_ZONEUPDATE(byte[] data)
        {
            using var reader = new PacketReader(data);
            NewZone = (ZoneID)reader.ReadUInt32();
        }

        public ZoneID NewZone { get; }
    }
}
