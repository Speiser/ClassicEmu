using Classic.Shared.Data;

namespace Classic.World.Packets.Server
{
    public class SMSG_INITIALIZE_FACTIONS : ServerPacketBase<Opcode>
    {
        private readonly int build;

        // Reputation package??
        public SMSG_INITIALIZE_FACTIONS(int build) : base(Opcode.SMSG_INITIALIZE_FACTIONS)
        {
            this.build = build;
        }

        public override byte[] Get()
        {
            if (build == ClientBuild.Vanilla)
            {
                return this.Writer
                    .WriteInt32(0) // Faction count
                    .Build();
            }

            // TBC
            this.Writer.WriteUInt32(0x00000080);

            for (var i = 0; i < 128; i++)
            {
                this.Writer.WriteUInt8(0).WriteUInt32(0);
            }

            return this.Writer.Build();
        }
    }
}
