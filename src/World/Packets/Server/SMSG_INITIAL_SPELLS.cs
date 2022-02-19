using System.Collections.Generic;
using Classic.World.Data;

namespace Classic.World.Packets.Server;

public class SMSG_INITIAL_SPELLS : ServerPacketBase<Opcode>
{
    private readonly List<Spell> spells;

    public SMSG_INITIAL_SPELLS(List<Spell> spells) : base(Opcode.SMSG_INITIAL_SPELLS)
    {
        this.spells = spells;
    }

    public override byte[] Get()
    {
        this.Writer
            .WriteUInt8(0) // ??
            .WriteUInt16((ushort)this.spells.Count);

        ushort slot = 1;
        foreach (var spell in this.spells)
        {
            this.Writer
                .WriteUInt16((ushort)spell.Id)
                .WriteUInt16(slot++);
        }

        this.Writer.WriteUInt16(0); // Cooldown count
        return this.Writer.Build();
    }
}
