using Classic.World.Entities.Enums;
using Classic.World.Entities.Utils;

namespace Classic.World.Entities
{
    internal class UnitEntity : ObjectEntity
    {
        public UnitEntity(ObjectGuid objectGuid) : base(objectGuid)
        {
        }

        public TypeId TypeId => TypeId.TypeidUnit;
        public override int DataLength => (int) UnitFields.UNIT_END - 0x4;
    }
}