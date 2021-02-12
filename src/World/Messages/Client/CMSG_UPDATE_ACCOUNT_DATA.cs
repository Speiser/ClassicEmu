using Classic.Common;

namespace Classic.World.Messages.Client
{
    public class CMSG_UPDATE_ACCOUNT_DATA
    {
        public CMSG_UPDATE_ACCOUNT_DATA(byte[] data)
        {
            using var reader = new PacketReader(data);
            Type = reader.ReadUInt32();
            Size = reader.ReadUInt32();
            // Data = ???
        }

        public uint Type { get; }
        public uint Size { get; }
        public string Data { get; }
    }
}
