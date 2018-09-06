namespace Classic.Auth
{
    public enum ClientState
    {
        Init,
        LogonChallenge,
        LogonProof,
        Authenticated,
        Disconnected
    }
}