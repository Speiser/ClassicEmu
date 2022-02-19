namespace Classic.World.Data.Enums.Character;

public enum CharacterFlag
{
    None = 0x0,

    /// <summary>
    ///     Character Locked for Paid Character Transfer
    /// </summary>
    LockedForTransfer = 0x4,

    HideHelm = 0x400,

    HideCloak = 0x800,

    /// <summary>
    ///     Player is ghost in char selection screen
    /// </summary>
    Ghost = 0x2000,

    /// <summary>
    ///     On login player will be asked to change name
    /// </summary>
    Rename = 0x4000,

    LockedByBilling = 0x1000000,

    Declined = 0x2000000
}
