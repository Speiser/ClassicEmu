using Classic.World.Entities.Enums;

namespace Classic.World.Entities;

public class ObjectEntity : BaseEntity
{
    protected ObjectEntity(ulong guid, int build) : base(build)
    {
        this.SetUpdateField((int)ObjectFields.Guid, guid);
    }

    protected override int GetDatalength(int build) => (int)ObjectFields.End;
}
