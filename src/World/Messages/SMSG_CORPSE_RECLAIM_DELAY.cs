using Classic.Common;

namespace Classic.World.Messages
{
    public class SMSG_CORPSE_RECLAIM_DELAY : ServerMessageBase<Opcode>
    {
        public SMSG_CORPSE_RECLAIM_DELAY() : base(Opcode.SMSG_CORPSE_RECLAIM_DELAY)
        {
        }

        public override byte[] Get() => this.Writer.WriteInt32(2000).Build();
    }
}