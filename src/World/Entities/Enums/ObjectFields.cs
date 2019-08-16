namespace Classic.World.Entities.Enums
{
    internal enum ObjectFields
    {
        Guid = 0x0, // 0x000 - Size: 2 - Type: GUID  - Flags: PUBLIC
        Type = 0x2, // 0x002 - Size: 1 - Type: INT   - Flags: PUBLIC
        Entry = 0x3, // 0x003 - Size: 1 - Type: INT   - Flags: PUBLIC
        ScaleX = 0x4, // 0x004 - Size: 1 - Type: FLOAT - Flags: PUBLIC
        Padding = 0x5, // 0x005 - Size: 1 - Type: INT   - Flags: NONE
        End = 0x6
    }
}