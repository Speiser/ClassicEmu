namespace Classic.Auth
{
    public enum ClientState
    {
        Init,
        ClientLogonChallenge,
        ServerLogonChallenge,
        ClientLogonProof,
        ServerLogonProof,
        Authenticated,
        Disconnected
    }
}