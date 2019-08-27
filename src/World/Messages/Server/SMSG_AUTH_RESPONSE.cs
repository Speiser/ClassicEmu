using Classic.Common;

namespace Classic.World.Messages.Server
{
    public class SMSG_AUTH_RESPONSE : ServerMessageBase<Opcode>
    {
        public SMSG_AUTH_RESPONSE() : base(Opcode.SMSG_AUTH_RESPONSE) {}

        public override byte[] Get() => this.Writer
                .WriteUInt8(12)
                .WriteUInt32(0)
                .WriteUInt8(0)
                .WriteUInt32(0)
                .Build();
    }
}
