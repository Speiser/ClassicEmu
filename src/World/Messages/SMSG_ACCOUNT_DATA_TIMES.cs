using Classic.Common;

namespace Classic.World.Messages
{
    public class SMSG_ACCOUNT_DATA_TIMES : ServerMessageBase<Opcode>
    {
        public SMSG_ACCOUNT_DATA_TIMES() : base(Opcode.SMSG_ACCOUNT_DATA_TIMES)
        {
        }

        public override byte[] Get() => this.Writer.WriteBytes(new byte[80]).Build();
    }
}