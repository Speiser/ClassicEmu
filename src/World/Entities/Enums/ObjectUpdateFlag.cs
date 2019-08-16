using System;

namespace Classic.World.Entities.Enums
{
    [Flags]
    internal enum ObjectUpdateFlag : byte
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
}