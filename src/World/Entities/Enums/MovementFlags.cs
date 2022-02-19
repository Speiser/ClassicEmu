using System;

namespace Classic.World.Entities.Enums;

[Flags]
public enum MovementFlags
{
    None = 0x00000000,
    Forward = 0x00000001,
    Backward = 0x00000002,
    StrafeLeft = 0x00000004,
    StrafeRight = 0x00000008,
    TurnLeft = 0x00000010,
    TurnRight = 0x00000020,
    PitchUp = 0x00000040,
    PitchDown = 0x00000080,
    WalkMode = 0x00000100, // Walking

    Levitating = 0x00000400,
    Root = 0x00000800, // [-ZERO] is it really need and correct value
    Falling = 0x00002000,
    Fallingfar = 0x00004000,
    Swimming = 0x00200000, // appears with fly flag also
    Ascending = 0x00400000, // [-ZERO] is it really need and correct value
    CanFly = 0x00800000, // [-ZERO] is it really need and correct value
    Flying = 0x01000000, // [-ZERO] is it really need and correct value

    Ontransport = 0x02000000, // Used for flying on some creatures
    SplineElevation = 0x04000000, // used for flight paths
    SplineEnabled = 0x08000000, // used for flight paths
    Waterwalking = 0x10000000, // prevent unit from falling through water
    SafeFall = 0x20000000, // active rogue safe fall spell (passive)
    Hover = 0x40000000
}
