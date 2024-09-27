using System.Collections.Generic;
using Classic.World.Extensions;

namespace Classic.World.Packets.Server;

public class SMSG_SPELLLOGEXECUTE(
        ulong casterId, uint spellId, SpellLogType logType, List<SpellLogTarget> targets)
    : ServerPacketBase<Opcode>(Opcode.SMSG_SPELLLOGEXECUTE)
{
    private readonly ulong casterId = casterId;
    private readonly uint spellId = spellId;
    private readonly SpellLogType logType = logType;
    private readonly List<SpellLogTarget> targets = targets;

    public override byte[] Get()
    {
        Writer
            .WriteBytes(casterId.ToPackedUInt64())
            .WriteUInt32(spellId)
            .WriteUInt32((uint)logType)
            .WriteUInt32((uint)targets.Count);

        foreach (var target in targets)
        {
            Writer
                .WriteBytes(target.TargetGuid.ToPackedUInt64())
                .WriteUInt32(target.EffectData);
        }
        return Writer.Build();
    }
}

public enum SpellLogType
{
    Damage = 0x00,
    Healing = 0x01,
    Buff = 0x02,
}

public class SpellLogTarget
{
    public ulong TargetGuid { get; set; }

    /// <summary>
    /// E.g damage dealt or healing done
    /// </summary>
    public uint EffectData { get; set; }
}
