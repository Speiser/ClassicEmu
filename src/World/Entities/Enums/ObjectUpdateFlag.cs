using System;

namespace Classic.World.Entities.Enums
{
    [Flags]
    public enum ObjectUpdateFlag_VANILLA : byte
    {
        None = 0x0000,
        Self = 0x0001,
        Transport = 0x0002,
        Fullguid = 0x0004,
        Highguid = 0x0008,
        All = 0x0010,
        Living = 0x0020,
        HasPosition = 0x0040
    }

    [Flags]
    public enum ObjectUpdateFlag_TBC : byte
    {
        None = 0x0000,
        Self = 0x0001,
        Transport = 0x0002,
        HAS_ATTACKING_TARGET = 0x0004,
        LOWGUID = 0x0008,
        Highguid = 0x0010,
        Living = 0x0020,
        HasPosition = 0x0040,
    }
}