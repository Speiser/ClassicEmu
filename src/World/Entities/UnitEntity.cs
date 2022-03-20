using System;
using Classic.Shared.Data;
using Classic.World.Data;
using Classic.World.Entities.Enums;

namespace Classic.World.Entities;

public class UnitEntity : ObjectEntity
{
    public UnitEntity(Creature unit, int build) : base(unit.ID, build)
    {
        this.SetUpdateField((int)ObjectFields.Type, (byte)(ObjectType.TYPE_OBJECT + (int)ObjectType.TYPE_UNIT));
        this.SetUpdateField((int)ObjectFields.ScaleX, 1f);

        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_DISPLAYID, unit.Model);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_NATIVEDISPLAYID, unit.Model);

        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_NPC_FLAGS, 0); //Npc
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_DYNAMIC_FLAGS, 0); // Dynamic
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_FLAGS, 0x8); // Unit

        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_FACTIONTEMPLATE, 954); // FactionAlliance

        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_HEALTH, unit.Life); // Health
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MAXHEALTH, unit.MaxLife); // Health
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_LEVEL, 5); // Level

        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_COMBATREACH, 10f);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_ATTACK_POWER, 0);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BYTES_2, 1);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MINDAMAGE, 10f);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MAXDAMAGE, 10f);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BASEATTACKTIME, 1000);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_OFFHANDATTACKTIME, 1000);

        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BYTES_0, 1); // TODO: unit_Class
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BYTES_1, 0);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BOUNDINGRADIUS, 30f);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_CREATED_BY_SPELL, 1);
    }

    protected override int GetDatalength(int build) => build switch
    {
        ClientBuild.Vanilla => (int)UnitFields_Vanilla.UNIT_END - 0x4,
        _ => throw new NotImplementedException($"UnitEntity.GetDatalength(build: {build})"),
    };
}
