namespace Classic.World.Messages.Server
{
    public class SMSG_CAST_RESULT : ServerMessageBase<Opcode>
    {
        public SMSG_CAST_RESULT() : base(Opcode.SMSG_CAST_RESULT)
        {
        }

        public static SMSG_CAST_RESULT Success(uint spellId)
        {
            var message = new SMSG_CAST_RESULT();
            message.Writer
                .WriteUInt32(spellId)
                .WriteUInt8((byte)SpellCastStatus.Success);
            return message;
        }

        public override byte[] Get() => this.Writer.Build();

        private enum SpellCastStatus
        {
            Success = 0,
            Fail = 2
        }
    }
}
