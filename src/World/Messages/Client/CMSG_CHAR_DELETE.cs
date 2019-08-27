using Classic.Common;

namespace Classic.World.Messages.Client
{
    public class CMSG_CHAR_DELETE
    {
        public CMSG_CHAR_DELETE(byte[] data)
        {
            using (var reader = new PacketReader(data))
            {
                CharacterID = reader.ReadUInt64();
            }
        }

        public ulong CharacterID { get; }
    }
}
