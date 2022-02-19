using Classic.Shared.Data;
using Classic.World.Extensions;

namespace Classic.World.Packets.Shared;

public class MovementUpdate : ServerPacketBase<Opcode>
{
    private readonly ulong characterId;
    private readonly MSG_MOVE_GENERIC original;
    private readonly int build;

    public MovementUpdate(ulong characterId, MSG_MOVE_GENERIC original, Opcode opcode, int build) : base(opcode)
    {
        this.characterId = characterId;
        this.original = original;
        this.build = build;
    }

    public override byte[] Get()
    {
        this.Writer
            .WriteBytes(this.characterId.ToPackedUInt64())
            .WriteUInt32((uint)this.original.MovementFlags)
            .WriteUInt32(this.original.Time); // Before moveflags2??

        if (this.build == ClientBuild.TBC)
            this.Writer.WriteUInt8(original.MovementFlags2);

        return this.Writer
            .WriteFloat(this.original.MapX)
            .WriteFloat(this.original.MapY)
            .WriteFloat(this.original.MapZ)
            .WriteFloat(this.original.MapO)
            .Build();
    }
}
