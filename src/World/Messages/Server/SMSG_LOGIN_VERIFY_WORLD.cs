using Classic.World.Data;

namespace Classic.World.Messages.Server
{
    public class SMSG_LOGIN_VERIFY_WORLD : ServerMessageBase<Opcode>
    {
        private readonly Character character;

        public SMSG_LOGIN_VERIFY_WORLD(Character character) : base(Opcode.SMSG_LOGIN_VERIFY_WORLD)
        {
            this.character = character;
        }

        public override byte[] Get() => this.Writer
            .WriteInt32((int)character.Position.ID) // MapID
            .WriteFloat(character.Position.X) // MapX
            .WriteFloat(character.Position.Y) // MapY
            .WriteFloat(character.Position.Z) // MapZ
            .WriteFloat(character.Position.Orientation) // MapO (Orientation)
            .Build();
    }
}