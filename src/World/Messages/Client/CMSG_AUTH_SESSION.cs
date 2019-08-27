using Classic.Common;

namespace Classic.World.Messages.Client
{
    public class CMSG_AUTH_SESSION
    {
        public CMSG_AUTH_SESSION(byte[] data)
        {
            using (var reader = new PacketReader(data))
            {
                Build = reader.ReadUInt32();
                Session = reader.ReadUInt32();
                AccountName = reader.ReadString();
                Seed = reader.ReadUInt32();
                Digest = reader.ReadBytes(20);
                AddonSize = reader.ReadUInt32();
            }
        }

        public uint Build { get; }
        public uint Session { get; }
        public string AccountName { get; }
        public uint Seed { get; }
        public byte[] Digest { get; }
        public uint AddonSize { get; }
    }
}
