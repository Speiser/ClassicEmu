namespace Classic.World.Packets.Server
{
    public class SMSG_GROUP_INVITE : ServerPacketBase<Opcode>
    {
        private readonly string sender;
        public SMSG_GROUP_INVITE(string sender) : base(Opcode.SMSG_GROUP_INVITE)
        {
            this.sender = sender;
        }

        public override byte[] Get() => this.Writer.WriteString(this.sender).Build();
    }
}