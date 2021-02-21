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

    public class SMSG_REALM_SPLIT : ServerMessageBase<Opcode>
    {
        // split states:
        // 0x0 realm normal
        // 0x1 realm split
        // 0x2 realm split pending
        private readonly uint decision;
        private const string SplitDate = "01/01/01";

        public SMSG_REALM_SPLIT(uint decision) : base(Opcode.SMSG_REALM_SPLIT)
        {
            this.decision = decision;
        }

        public override byte[] Get() => this.Writer
            .WriteUInt32(this.decision)
            .WriteUInt32(0) // Status
            .WriteString(SplitDate)
            .Build();
    }
}