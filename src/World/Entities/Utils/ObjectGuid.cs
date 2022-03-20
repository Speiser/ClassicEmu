using Classic.World.Entities.Enums;

namespace Classic.World.Entities.Utils;

// DELETE?
public class ObjectGuid
{
    public ObjectGuid(ulong guid)
    {
        RawGuid = guid;
    }

    public ObjectGuid(uint index, TypeId type, HighGuid high)
    {
        TypeId = type;
        HighGuid = high;
        RawGuid = index | ((ulong) type << 24) | ((ulong) high << 48);
    }

    public TypeId TypeId { get; }
    public HighGuid HighGuid { get; }
    public ulong RawGuid { get; }
}
