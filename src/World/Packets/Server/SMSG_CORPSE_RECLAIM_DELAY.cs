namespace Classic.World.Packets.Server
{
    public class SMSG_CORPSE_RECLAIM_DELAY : ServerPacketBase<Opcode>
    {
        public SMSG_CORPSE_RECLAIM_DELAY() : base(Opcode.SMSG_CORPSE_RECLAIM_DELAY)
        {
        }

        public override byte[] Get() => this.Writer.WriteInt32(2000).Build();
    }
}