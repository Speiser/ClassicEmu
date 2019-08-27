using Classic.Common;
using Classic.Data;

namespace Classic.World.Messages.Server
{
    public class SMSG_BINDPOINTUPDATE : ServerMessageBase<Opcode>
    {
        private readonly Character character;

        public SMSG_BINDPOINTUPDATE(Character character) : base(Opcode.SMSG_BINDPOINTUPDATE)
        {
            this.character = character;
        }

        public override byte[] Get() => this.Writer
            .WriteFloat(character.Position.X) // MapX
            .WriteFloat(character.Position.Y) // MapY
            .WriteFloat(character.Position.Z) // MapZ
            .WriteUInt32((uint)character.Position.ID) // MapID
            .WriteUInt32((uint)character.Position.Zone) // ZoneID
            .Build();
    }
}