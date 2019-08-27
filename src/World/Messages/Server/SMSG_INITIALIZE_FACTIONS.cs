using Classic.Common;

namespace Classic.World.Messages.Server
{
    public class SMSG_INITIALIZE_FACTIONS : ServerMessageBase<Opcode>
    {
        // Reputation package??
        public SMSG_INITIALIZE_FACTIONS() : base(Opcode.SMSG_INITIALIZE_FACTIONS)
        {
        }

        public override byte[] Get()
            => this.Writer
                .WriteInt32(0) // Faction count
                .Build();
    }
}
