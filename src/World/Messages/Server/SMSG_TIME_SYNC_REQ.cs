namespace Classic.World.Messages.Server
{
    public class SMSG_TIME_SYNC_REQ : ServerMessageBase<Opcode>
    {
        public SMSG_TIME_SYNC_REQ() : base(Opcode.SMSG_TIME_SYNC_REQ) { }
        public override byte[] Get() => this.Writer.WriteUInt32(5000).Build();
    }
}