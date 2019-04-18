using Classic.Common;

namespace Classic.World.Messages
{
    public class SMSG_AUTH_CHALLENGE : ServerMessageBase<Opcode>
    {
        public SMSG_AUTH_CHALLENGE() : base(Opcode.SMSG_AUTH_CHALLENGE)
        {
        }

        public static byte[] AuthSeed => new byte[] { 0x33, 0x18, 0x34, 0xC8 };

        public override byte[] Get() => this.Writer
            .WriteUInt16Reversed(6) // length
            .WriteUInt16((ushort)this.Opcode)
            .WriteBytes(AuthSeed)
            .Build();
    }
}
