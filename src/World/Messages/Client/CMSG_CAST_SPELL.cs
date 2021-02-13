using Classic.Common;

namespace Classic.World.Messages.Client
{
    public class CMSG_CAST_SPELL
    {
        public CMSG_CAST_SPELL(byte[] data)
        {
            using var reader = new PacketReader(data);
            SpellId = reader.ReadUInt32();
            // ?? rest unknown ??
        }

        public uint SpellId { get; }
    }
}
