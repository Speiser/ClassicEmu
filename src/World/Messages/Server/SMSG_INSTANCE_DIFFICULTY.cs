namespace Classic.World.Messages.Server
{
    public class SMSG_INSTANCE_DIFFICULTY : ServerMessageBase<Opcode>
    {
        public SMSG_INSTANCE_DIFFICULTY() : base(Opcode.SMSG_INSTANCE_DIFFICULTY) { }

        public override byte[] Get() => this.Writer
            .WriteUInt32(0) // Instance difficulty (0 == DUNGEON_DIFFICULTY_NORMAL)
            .WriteUInt32(0) // ????
            .Build();
    }
}