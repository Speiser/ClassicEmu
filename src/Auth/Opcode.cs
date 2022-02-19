namespace Classic.Auth;

// https://wowdev.wiki/Packets/Login/Vanilla
public enum Opcode
{
    LoginChallenge = 0x00,
    LoginProof = 0x01,
    ReconnectChallenge = 0x02,
    ReconnectProof = 0x03,
    Realmlist = 0x10
}
