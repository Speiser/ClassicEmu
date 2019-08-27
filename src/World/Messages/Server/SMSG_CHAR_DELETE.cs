using Classic.Common;

namespace Classic.World.Messages.Server
{
    public class SMSG_CHAR_DELETE : ServerMessageBase<Opcode>
    {
        public SMSG_CHAR_DELETE() : base(Opcode.SMSG_CHAR_DELETE)
        {
        }

        public static SMSG_CHAR_DELETE Success()
        {
            var message = new SMSG_CHAR_DELETE();
            message.Writer.WriteUInt8(57);
            return message;
        }

        public static SMSG_CHAR_DELETE Fail()
        {
            var message = new SMSG_CHAR_DELETE();
            message.Writer.WriteUInt8(58);
            return message;
        }

        public override byte[] Get() => this.Writer.Build();
    }
}
