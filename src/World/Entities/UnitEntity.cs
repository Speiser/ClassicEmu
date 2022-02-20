using System;
using Classic.Shared.Data;
using Classic.World.Data;
using Classic.World.Entities.Enums;
using Classic.World.Entities.Utils;

namespace Classic.World.Entities;

public class UnitEntity : ObjectEntity
{
    protected UnitEntity(ObjectGuid objectGuid, int build) : base(objectGuid, build) { }

    public UnitEntity(Creature unit, int build)
        : base(new ObjectGuid((uint)unit.ID, TypeId.TypeidUnit, HighGuid.HighguidUnit), build)
    {
        Type = (byte)(ObjectType.TYPE_OBJECT + (int)ObjectType.TYPE_UNIT);
        Scale = 1f;

        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_DISPLAYID, unit.Model);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_NATIVEDISPLAYID, unit.Model);

        SetUpdateField((int)UnitFields_Vanilla.UNIT_NPC_FLAGS, 0); //Npc
        SetUpdateField((int)UnitFields_Vanilla.UNIT_DYNAMIC_FLAGS, 0); // Dynamic
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_FLAGS, 0x8); // Unit

        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_FACTIONTEMPLATE, 954); // FactionAlliance

        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_HEALTH, unit.Life); // Health
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MAXHEALTH, unit.MaxLife); // Health
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_LEVEL, 5); // Level

        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_COMBATREACH, 10f);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_ATTACK_POWER, 0);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BYTES_2, 1);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MINDAMAGE, 10f);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MAXDAMAGE, 10f);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BASEATTACKTIME, 1000);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BASEATTACKTIME + 1, 1000);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BOUNDINGRADIUS, 1f);

        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BYTES_0, 1); // TODO: unit_Class
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BYTES_1, 0);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BOUNDINGRADIUS, 30f);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_CREATED_BY_SPELL, 1);
    }

    public TypeId TypeId => TypeId.TypeidUnit;

    protected override int GetDatalength(int build) => build switch
    {
        ClientBuild.Vanilla => (int)UnitFields_Vanilla.UNIT_END - 0x4,
        _ => throw new NotImplementedException($"UnitEntity.GetDatalength(build: {build})"),
    };
}
