using Classic.Shared;

namespace Classic.World.Messages.Client
{
    public class CMSG_CAST_SPELL
    {
        public CMSG_CAST_SPELL(byte[] data)
        {
            using var reader = new PacketReader(data);
            SpellId = reader.ReadUInt32();
            this.Unk1 = reader.ReadByte();
            this.Unk2 = reader.ReadByte();
            this.Unk3 = reader.ReadByte();
            this.TargetId = reader.ReadUInt64();
        }

        public uint SpellId { get; }
        public byte Unk1 { get; }
        public byte Unk2 { get; }
        public byte Unk3 { get; }
        public ulong TargetId { get; }
    }
}
