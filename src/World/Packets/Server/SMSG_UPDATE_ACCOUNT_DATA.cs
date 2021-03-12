namespace Classic.World.Packets.Server
{
    public class SMSG_UPDATE_ACCOUNT_DATA : ServerPacketBase<Opcode>
    {
        public SMSG_UPDATE_ACCOUNT_DATA() : base(Opcode.SMSG_UPDATE_ACCOUNT_DATA)
        {
        }

        public override byte[] Get() => this.Writer.Build();
    }
}
