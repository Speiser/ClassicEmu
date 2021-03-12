namespace Classic.World.Packets.Server
{
    public class SMSG_PONG : ServerPacketBase<Opcode>
    {
        private readonly uint latency;
        public SMSG_PONG(uint latency) : base(Opcode.SMSG_PONG)
        {
            this.latency = latency;
        }

        public override byte[] Get() => this.Writer.WriteUInt64(latency).Build();
    }
}
