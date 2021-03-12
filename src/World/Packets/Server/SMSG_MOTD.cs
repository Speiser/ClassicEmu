namespace Classic.World.Packets.Server
{
    // NOT IMPLEMENTED FOR VANILLA
    public class SMSG_MOTD : ServerPacketBase<Opcode>
    {
        private readonly string[] lines;

        public SMSG_MOTD(params string[] lines) : base(Opcode.SMSG_MOTD)
        {
            this.lines = lines;
        }

        public override byte[] Get()
        {
            this.Writer.WriteUInt32((uint)this.lines.Length);

            foreach (var line in this.lines)
            {
                this.Writer.WriteString(line);
            }

            return this.Writer.Build();
        }
    }
}