namespace Classic.World.Packets.Server;

public class SMSG_AUTH_CHALLENGE : ServerPacketBase<Opcode>
{
    // Length and Opcode needs to be written here, as it is sent without `SendPacket`
    public SMSG_AUTH_CHALLENGE() : base(Opcode.SMSG_AUTH_CHALLENGE)
    {
    }

    public static byte[] AuthSeed => new byte[] { 0x33, 0x18, 0x34, 0xC8 };

    public static SMSG_AUTH_CHALLENGE VanillaTBC()
    {
        var packet = new SMSG_AUTH_CHALLENGE();
        packet.Writer
            .WriteUInt16Reversed(6) // length
            .WriteUInt16((ushort)Opcode.SMSG_AUTH_CHALLENGE)
            .WriteBytes(AuthSeed);
        return packet;
    }

    public static SMSG_AUTH_CHALLENGE WotLK()
    {
        var packet = new SMSG_AUTH_CHALLENGE();
        packet.Writer
            .WriteUInt16Reversed(40) // length
            .WriteUInt16((ushort)Opcode.SMSG_AUTH_CHALLENGE)
            .WriteUInt32(1) // 1..31 ???
            .WriteBytes(AuthSeed) // m_seed ???
            .WriteBytes(Classic.Shared.Cryptography.Random.GetBytes(16))  // seed1
            .WriteBytes(Classic.Shared.Cryptography.Random.GetBytes(16)); // seed2
        return packet;
    }

    public override byte[] Get() => this.Writer.Build();
}
