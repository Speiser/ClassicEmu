using Classic.Common;

namespace Classic.World.Messages
{
    public class SMSG_CHAR_CREATE : ServerMessageBase<Opcode>
    {
        public SMSG_CHAR_CREATE() : base(Opcode.SMSG_CHAR_CREATE)
        {
        }

        public override byte[] Get() =>
            // TODO: Always returns success atm
            this.Writer.WriteUInt8(46).Build();
    }
}
