using Classic.Common;

namespace Classic.World.Messages.Server
{
    public class SMSG_PONG : ServerMessageBase<Opcode>
    {
        private readonly uint latency;
        public SMSG_PONG(uint latency) : base(Opcode.SMSG_PONG)
        {
            this.latency = latency;
        }

        public override byte[] Get() => this.Writer.WriteUInt64(latency).Build();
    }
}
