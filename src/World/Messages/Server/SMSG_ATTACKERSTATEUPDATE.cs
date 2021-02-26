using Classic.Common;
using Classic.World.Extensions;

namespace Classic.World.Messages.Server
{
    public class SMSG_ATTACKERSTATEUPDATE : ServerMessageBase<Opcode>
    {
        private readonly ulong playerId;
        private readonly ulong targetId;

        public SMSG_ATTACKERSTATEUPDATE(ulong playerId, ulong targetId) : base(Opcode.SMSG_ATTACKERSTATEUPDATE)
        {
            this.playerId = playerId;
            this.targetId = targetId;
        }

        public override byte[] Get()
        {
            this.Writer
                .WriteUInt32(0x00000002) // HITINFO_NORMALSWING2
                .WriteBytes(playerId.ToPackedUInt64())
                .WriteBytes(targetId.ToPackedUInt64())
                .WriteUInt32(10) // Total damage
                .WriteUInt8(1) // lines.Count
                    // foreach line in lines
                    .WriteUInt32(0) // spell school (0 == normal)
                    .WriteFloat(1) // Float coefficient of subdamage (line.damage/totalDamage)
                    .WriteUInt32(10) // line.damage
                    .WriteUInt32(0) // line.absorb
                    .WriteUInt32(0) // line.resist
                .WriteUInt32(0) // targetstate (0 == is unaffected)
                .WriteUInt32(0) // unk
                .WriteUInt32(0) // spell id??
                .WriteUInt32(0) // blocked amount
                ;

            return this.Writer.Build();
        }
    }
}