using System;
using Classic.World.Extensions;

namespace Classic.World.Packets.Server;

public class SMSG_SPELL_START(ulong casterGuid, ulong targetGuid, uint spellId, uint castTime) : ServerPacketBase<Opcode>(Opcode.SMSG_SPELL_START)
{
    private readonly ulong casterGuid = casterGuid;

    /// <summary>
    /// 0 for no target.
    /// </summary>
    private readonly ulong targetGuid = targetGuid;
    private readonly uint spellId = spellId;

    /// <summary>
    /// Cast time in milliseconds.
    /// 0 for instant cast.
    /// </summary>
    private readonly uint castTime = castTime;

    public override byte[] Get() => Writer
        .WriteBytes(casterGuid.ToPackedUInt64())
        .WriteBytes(casterGuid.ToPackedUInt64())
        .WriteUInt32(spellId)
        .WriteUInt16((ushort)SpellCastFlags.CAST_FLAG_AMMO)
        .WriteUInt32(castTime)
        .WriteBytes(targetGuid.ToPackedUInt64())
        .Build();
}

[Flags]
public enum SpellCastFlags
{
    CAST_FLAG_NONE = 0x00,
    CAST_FLAG_HIDDEN_COMBATLOG = 0x01,
    CAST_FLAG_UNKNOWN2 = 0x02,
    CAST_FLAG_UNKNOWN3 = 0x04,
    CAST_FLAG_UNKNOWN4 = 0x08,
    CAST_FLAG_UNKNOWN5 = 0x10,
    CAST_FLAG_AMMO = 0x20,
}
