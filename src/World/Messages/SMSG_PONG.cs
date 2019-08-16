using Classic.Common;

namespace Classic.World.Messages
{
    public class SMSG_PONG : ServerMessageBase<Opcode>
    {
        private readonly uint latency;
        public SMSG_PONG(byte[] data) : base(Opcode.SMSG_PONG)
        {
            using (var reader = new PacketReader(data))
            {
                var ping = reader.ReadUInt32();
                latency = reader.ReadUInt32();
            }
        }

        public override byte[] Get() => this.Writer.WriteUInt64(latency).Build();
    }
}
