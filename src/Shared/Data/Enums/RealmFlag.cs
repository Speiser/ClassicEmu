using System;

namespace Classic.Shared.Data.Enums;

[Flags]
public enum RealmFlag
{
    None = 0x00,
    VersionMismatch = 0x01,
    Offline = 0x02,
    Specifybuild = 0x04,
    Unknown1 = 0x08,
    Unknown2 = 0x10,
    Recommended = 0x20,
    New = 0x40,
    Full = 0x80,
}
