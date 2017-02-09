namespace ClassicEmu.Server.Enums
{
    public enum ServerLogonChallengeErrorCode
    {
        CE_SUCCESS = 0x00,
        CE_IPBAN = 0x01, //2bd -- unable to connect (some internal problem)
        CE_ACCOUNT_CLOSED = 0x03, // "This account has been closed and is no longer in service -- Please check the registered email address of this account for further information.";
        CE_NO_ACCOUNT = 0x04, //(5)The information you have entered is not valid.  Please check the spelling of the account name and password.  If you need help in retrieving a lost or stolen password and account
        CE_ACCOUNT_IN_USE = 0x06, //This account is already logged in.  Please check the spelling and try again.
        CE_PREORDER_TIME_LIMIT = 0x07,
        CE_SERVER_FULL = 0x08, //Could not log in at this time.  Please try again later.
        CE_WRONG_BUILD_NUMBER = 0x09, //Unable to validate game version.  This may be caused by file corruption or the interference of another program.
        CE_UPDATE_CLIENT = 0x0a,
        CE_ACCOUNT_FREEZED = 0x0c
    }
}
