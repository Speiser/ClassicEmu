using Classic.World.Extensions;

namespace Classic.World.Packets.Server;

public class SMSG_SPELL_GO(ulong casterGuid, ulong targetGuid, uint spellId) : ServerPacketBase<Opcode>(Opcode.SMSG_SPELL_GO)
{
    private readonly ulong casterGuid = casterGuid;
    private readonly ulong targetGuid = targetGuid;
    private readonly uint spellId = spellId;

    public override byte[] Get() => Writer
        .WriteBytes(casterGuid.ToPackedUInt64())
        .WriteBytes(casterGuid.ToPackedUInt64())
        .WriteUInt32(spellId)
        .WriteUInt16((ushort)SpellCastFlags.CAST_FLAG_AMMO)
        .WriteUInt32(0) // HitInfo
        .WriteUInt32(1) // Target count?
        .WriteBytes(targetGuid.ToPackedUInt64()) // TODO: Iterate over list because could be multiple
        .Build();
}

