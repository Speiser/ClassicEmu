namespace Classic.World.Packets.Server
{
    public class SMSG_CHAR_CREATE : ServerPacketBase<Opcode>
    {
        private readonly byte status;

        public SMSG_CHAR_CREATE(byte status) : base(Opcode.SMSG_CHAR_CREATE)
        {
            this.status = status;
        }

        public override byte[] Get() => this.Writer.WriteUInt8(this.status).Build();
    }
}
