namespace Classic.World.Entities.Enums;

public enum ObjectUpdateType
{
    UPDATETYPE_VALUES = 0,

    //  1 byte  - MASK
    //  8 bytes - GUID
    //  Goto Update Block
    UPDATETYPE_MOVEMENT = 1,

    //  1 byte  - MASK
    //  8 bytes - GUID
    //  Goto Position Update
    UPDATETYPE_CREATE_OBJECT = 2,
    UPDATETYPE_CREATE_OBJECT_SELF = 3,

    //  1 byte  - MASK
    //  8 bytes - GUID
    //  1 byte - Object Type (*)
    //  Goto Position Update
    //  Goto Update Block
    UPDATETYPE_OUT_OF_RANGE_OBJECTS = 4,

    //  4 bytes - Count
    //  Loop Count Times:
    //  1 byte  - MASK
    //  8 bytes - GUID
    UPDATETYPE_NEAR_OBJECTS = 5 // looks like 4 & 5 do the same thing
}
