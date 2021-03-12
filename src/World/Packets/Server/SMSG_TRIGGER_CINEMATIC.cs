using Classic.World.Data.Enums;

namespace Classic.World.Packets.Server
{
    public class SMSG_TRIGGER_CINEMATIC : ServerPacketBase<Opcode>
    {
        private readonly CinematicID cinematicId;

        public SMSG_TRIGGER_CINEMATIC(CinematicID cinematicId) : base(Opcode.SMSG_TUTORIAL_FLAGS)
        {
            this.cinematicId = cinematicId;
        }

        public override byte[] Get() => this.Writer
            .WriteInt32((int)this.cinematicId)
            .Build();
    }
}
