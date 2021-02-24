using Classic.World.Entities.Enums;
using Classic.World.Entities.Utils;

namespace Classic.World.Entities
{
    public class ObjectEntity : BaseEntity
    {
        protected ObjectEntity(ObjectGuid objectGuid, int build) : base(build)
        {
            ObjectGuid = objectGuid;
            Guid = ObjectGuid.RawGuid;
        }

        public ObjectGuid ObjectGuid { get; set; }

        public ulong Guid
        {
            get => (ulong) UpdateData[ObjectFields.Guid];
            set => SetUpdateField((int) ObjectFields.Guid, value);
        }

        public byte Type
        {
            get => (byte) UpdateData[(int) ObjectFields.Type];
            set => SetUpdateField((int) ObjectFields.Type, value);
        }

        public int Entry
        {
            get => (int) UpdateData[(int) ObjectFields.Entry];
            set => SetUpdateField((int) ObjectFields.Entry, value);
        }

        public float Scale
        {
            get => (float) UpdateData[(int) ObjectFields.ScaleX];
            set => SetUpdateField((int) ObjectFields.ScaleX, value);
        }

        protected override int GetDatalength(int build) => (int)ObjectFields.End;
    }
}