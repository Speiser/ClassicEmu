namespace Classic.Auth.Data.Enums
{
    public enum AuthenticationStatus
    {
        Success = 0x00,
        FailedUnknown0 = 0x01,
        FailedUnknown1 = 0x02,
        FailedBanned = 0x03,
        FailedUnknownAccount = 0x04,
        FailedIncorrectPassword = 0x05,
        FailedAlreadyOnline = 0x06,
        FailedNoTime = 0x07,
        FailedDatabaseBusy = 0x08,
        FailedVersionInvalid = 0x09, // TODO: Send for unsupported Build
        FailedVersionUpdate = 0x0A,
        FailedInvalidServer = 0x0B,
        FailedSuspended = 0x0C,
        FailedNoAccess = 0x0D,
        SuccessSurvey = 0x0E,
        FailedParentcontrol = 0x0F,
        FailedLockedEnforced = 0x10,

        // Added after Vanilla
        FailedTrialEnded = 0x11,
        FailedUseBnet = 0x12,
    }
}
