namespace Classic.World.Packets.Server
{
    public class SMSG_AUTH_CHALLENGE_VANILLA_TBC : ServerPacketBase<Opcode>
    {
        public SMSG_AUTH_CHALLENGE_VANILLA_TBC() : base(Opcode.SMSG_AUTH_CHALLENGE)
        {
        }

        public static byte[] AuthSeed => new byte[] { 0x33, 0x18, 0x34, 0xC8 };

        public override byte[] Get() => this.Writer
            .WriteUInt16Reversed(6) // length
            .WriteUInt16((ushort)this.Opcode)
            .WriteBytes(AuthSeed)
            .Build();
    }

    public class SMSG_AUTH_CHALLENGE_WOTLK : ServerPacketBase<Opcode>
    {
        public SMSG_AUTH_CHALLENGE_WOTLK() : base(Opcode.SMSG_AUTH_CHALLENGE)
        {
        }

        public override byte[] Get() => this.Writer
            .WriteUInt16Reversed(40) // length
            .WriteUInt16((ushort)this.Opcode)
            .WriteUInt32(1) // 1..31 ???
            .WriteUInt32(3) // m_seed ???
            .WriteBytes(Classic.Shared.Cryptography.Random.GetBytes(16)) // seed1
            .WriteBytes(Classic.Shared.Cryptography.Random.GetBytes(16)) // seed2
            .Build();
    }
}
