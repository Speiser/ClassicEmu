namespace Classic.Auth
{
    // https://wowdev.wiki/Packets/Login/Vanilla
    public enum Opcode
    {
        LOGIN_CHALL = 0x00,
        LOGIN_PROOF = 0x01,
        RECONNECT_CHALLENGE = 0x02,
        RECONNECT_PROOF = 0x03,
        REALMLIST = 0x10
    }
}
