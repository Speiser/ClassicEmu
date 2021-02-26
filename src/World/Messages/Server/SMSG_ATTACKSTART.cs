using Classic.Common;

namespace Classic.World.Messages.Server
{
    public class SMSG_ATTACKSTART : ServerMessageBase<Opcode>
    {
        private readonly ulong playerId;
        private readonly ulong targetId;

        public SMSG_ATTACKSTART(ulong playerId, ulong targetId) : base(Opcode.SMSG_ATTACKSTART)
        {
            this.playerId = playerId;
            this.targetId = targetId;
        }

        public override byte[] Get() => this.Writer
            .WriteUInt64(this.playerId)
            .WriteUInt64(this.targetId)
            .Build();
    }
}