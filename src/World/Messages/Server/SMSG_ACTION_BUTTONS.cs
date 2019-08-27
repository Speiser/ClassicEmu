using Classic.Common;

namespace Classic.World.Messages.Server
{
    public class SMSG_ACTION_BUTTONS : ServerMessageBase<Opcode>
    {
        public SMSG_ACTION_BUTTONS() : base(Opcode.SMSG_ACTION_BUTTONS)
        {
        }

        public override byte[] Get()
        {
            for (var i = 0; i < 120; i++)
            {
                this.Writer.WriteUInt32(0);
            }

            return this.Writer.Build();
        }
    }
}
