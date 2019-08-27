using Classic.Common;

namespace Classic.World.Messages.Server
{
    public class SMSG_UPDATE_ACCOUNT_DATA : ServerMessageBase<Opcode>
    {
        // Reputation package??
        public SMSG_UPDATE_ACCOUNT_DATA() : base(Opcode.SMSG_UPDATE_ACCOUNT_DATA)
        {
        }

        public override byte[] Get() => this.Writer.Build();
    }
}
