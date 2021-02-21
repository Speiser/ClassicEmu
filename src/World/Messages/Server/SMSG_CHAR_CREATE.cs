using Classic.Common;

namespace Classic.World.Messages.Server
{
    public class SMSG_CHAR_CREATE : ServerMessageBase<Opcode>
    {
        private readonly byte status;

        public SMSG_CHAR_CREATE(byte status) : base(Opcode.SMSG_CHAR_CREATE)
        {
            this.status = status;
        }

        public override byte[] Get() => this.Writer.WriteUInt8(this.status).Build();
    }
}
