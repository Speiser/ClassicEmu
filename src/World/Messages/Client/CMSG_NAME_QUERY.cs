using Classic.Common;

namespace Classic.World.Messages.Client
{
    public class CMSG_NAME_QUERY
    {
        public CMSG_NAME_QUERY(byte[] data)
        {
            using (var reader = new PacketReader(data))
            {
                CharacterID = reader.ReadUInt64();
            }
        }

        public ulong CharacterID { get; }
    }
}
