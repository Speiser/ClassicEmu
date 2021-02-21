using Classic.Common;

namespace Classic.World.Messages.Server
{
    public class SMSG_ACCOUNT_DATA_TIMES : ServerMessageBase<Opcode>
    {
        public SMSG_ACCOUNT_DATA_TIMES() : base(Opcode.SMSG_ACCOUNT_DATA_TIMES)
        {
        }

        public override byte[] Get()
        {
            for (var i = 0; i < 32; i++)
            {
                this.Writer.WriteUInt32(0);
            }

            return this.Writer.Build();
        }
    }
}