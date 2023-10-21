using Classic.World.Extensions;

namespace Classic.World.Packets.Server;

public class SMSG_ATTACKSTOP : ServerPacketBase<Opcode>
{
    private readonly ulong playerId;
    private readonly ulong targetId;

    public SMSG_ATTACKSTOP(ulong playerId, ulong targetId) : base(Opcode.SMSG_ATTACKSTOP)
    {
        this.playerId = playerId;
        this.targetId = targetId;
    }

    public override byte[] Get() => this.Writer
        .WriteBytes(this.playerId.ToPackedUInt64())
        .WriteBytes(this.targetId.ToPackedUInt64())
        .WriteUInt32(0)
        .Build();
}
