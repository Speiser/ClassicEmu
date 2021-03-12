namespace Classic.World.Packets.Server
{
    public class SMSG_REALM_SPLIT : ServerPacketBase<Opcode>
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