using Classic.Common;

namespace Classic.World.Messages.Server
{
    // Added in 2.2.0
    public class SMSG_FEATURE_SYSTEM_STATUS : ServerMessageBase<Opcode>
    {
        public SMSG_FEATURE_SYSTEM_STATUS() : base(Opcode.SMSG_FEATURE_SYSTEM_STATUS) { }

        public override byte[] Get() => this.Writer
            .WriteUInt8(2) // Can complain
            .WriteUInt8(0) // Voice chat
            .Build();
    }
}