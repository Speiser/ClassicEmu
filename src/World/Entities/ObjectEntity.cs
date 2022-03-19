using Classic.World.Entities.Enums;
using Classic.World.Entities.Utils;

namespace Classic.World.Entities;

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

    protected override int GetDatalength(int build) => (int)ObjectFields.End;
}
