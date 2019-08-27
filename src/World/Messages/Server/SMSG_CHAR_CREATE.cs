using Classic.Common;

namespace Classic.World.Messages.Server
{
    public class SMSG_CHAR_CREATE : ServerMessageBase<Opcode>
    {
        public SMSG_CHAR_CREATE() : base(Opcode.SMSG_CHAR_CREATE)
        {
        }

        public static SMSG_CHAR_CREATE Success()
        {
            var message = new SMSG_CHAR_CREATE();
            message.Writer.WriteUInt8(46);
            return message;
        }

        public override byte[] Get() => this.Writer.Build();
    }
}
