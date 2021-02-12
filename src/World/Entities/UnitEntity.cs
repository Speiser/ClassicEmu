using Classic.World.Entities.Enums;
using Classic.World.Entities.Utils;

namespace Classic.World.Entities
{
    public class UnitEntity : ObjectEntity
    {
        public UnitEntity(ObjectGuid objectGuid) : base(objectGuid)
        {
        }

        public UnitEntity(ulong creatureId)
            : base(new ObjectGuid((uint)creatureId, TypeId.TypeidUnit, HighGuid.HighguidUnit))
        {
            var model = 169;

            Type = (byte)(ObjectType.TYPE_OBJECT + (int)ObjectType.TYPE_UNIT);
            Scale = 1f;

            SetUpdateField((int)UnitFields.UNIT_FIELD_DISPLAYID, model);
            SetUpdateField((int)UnitFields.UNIT_FIELD_NATIVEDISPLAYID, model);

            SetUpdateField((int)UnitFields.UNIT_NPC_FLAGS, 0); //Npc
            SetUpdateField((int)UnitFields.UNIT_DYNAMIC_FLAGS, 0); // Dynamic
            SetUpdateField((int)UnitFields.UNIT_FIELD_FLAGS, 0x8); // Unit

            SetUpdateField((int)UnitFields.UNIT_FIELD_FACTIONTEMPLATE, 954); // FactionAlliance

            SetUpdateField((int)UnitFields.UNIT_FIELD_HEALTH, 3517); // Health
            SetUpdateField((int)UnitFields.UNIT_FIELD_MAXHEALTH, 3517); // Health
            SetUpdateField((int)UnitFields.UNIT_FIELD_LEVEL, 50); // Level

            SetUpdateField((int)UnitFields.UNIT_FIELD_COMBATREACH, 10f);
            SetUpdateField((int)UnitFields.UNIT_FIELD_ATTACK_POWER, 0);
            SetUpdateField((int)UnitFields.UNIT_FIELD_BYTES_2, 1);
            SetUpdateField((int)UnitFields.UNIT_FIELD_MINDAMAGE, 10f);
            SetUpdateField((int)UnitFields.UNIT_FIELD_MAXDAMAGE, 10f);
            SetUpdateField((int)UnitFields.UNIT_FIELD_BASEATTACKTIME, 1000);
            SetUpdateField((int)UnitFields.UNIT_FIELD_BASEATTACKTIME + 1, 1000);
            SetUpdateField((int)UnitFields.UNIT_FIELD_BOUNDINGRADIUS, 1f);

            SetUpdateField((int)UnitFields.UNIT_FIELD_BYTES_0, 1); // TODO: unit_Class
            SetUpdateField((int)UnitFields.UNIT_FIELD_BYTES_1, 0);
            SetUpdateField((int)UnitFields.UNIT_FIELD_BOUNDINGRADIUS, 30f);
            SetUpdateField((int)UnitFields.UNIT_CREATED_BY_SPELL, 1);
        }

        public TypeId TypeId => TypeId.TypeidUnit;
        public override int DataLength => (int) UnitFields.UNIT_END - 0x4;
    }
}