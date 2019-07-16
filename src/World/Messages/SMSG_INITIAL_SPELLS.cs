using Classic.Common;

namespace Classic.World.Messages
{
    public class SMSG_INITIAL_SPELLS : ServerMessageBase<Opcode>
    {
        public SMSG_INITIAL_SPELLS() : base(Opcode.SMSG_INITIAL_SPELLS)
        {
        }

        public override byte[] Get()
            => this.Writer
                .WriteUInt8(0) // ??
                .WriteUInt16(0) // Spellcount
                .WriteUInt16(0) // ??
                .Build();
    }
}
