using Classic.Common;
using Classic.Data.Enums;

namespace Classic.World.Messages.Server
{
    public class SMSG_TRIGGER_CINEMATIC : ServerMessageBase<Opcode>
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
