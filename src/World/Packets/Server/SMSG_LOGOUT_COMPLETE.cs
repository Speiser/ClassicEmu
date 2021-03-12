namespace Classic.World.Packets.Server
{
    public class SMSG_LOGOUT_COMPLETE : ServerPacketBase<Opcode>
    {
        public SMSG_LOGOUT_COMPLETE() : base(Opcode.SMSG_LOGOUT_COMPLETE) { }

        public static SMSG_LOGOUT_COMPLETE Success()
        {
            var message = new SMSG_LOGOUT_COMPLETE();
            message.Writer.WriteUInt8(0);
            return message;
        }

        public override byte[] Get() => this.Writer.Build();
    }
}
