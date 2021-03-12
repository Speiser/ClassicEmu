using Classic.Shared.Data;
using Classic.World.Data;

namespace Classic.World.Packets.Server
{
    // TODO: Used during OnPlayerLogin?
    public class SMSG_NAME_QUERY_RESPONSE : ServerPacketBase<Opcode>
    {
        private readonly Character character;
        private readonly int build;

        public SMSG_NAME_QUERY_RESPONSE(Character character, int build) : base(Opcode.SMSG_NAME_QUERY_RESPONSE)
        {
            this.character = character;
            this.build = build;
        }

        public override byte[] Get()
        {
            this.Writer
                .WriteUInt64(character.Id)
                .WriteString(character.Name)
                .WriteUInt8(0)
                .WriteUInt32((uint)character.Race)
                .WriteUInt32((uint)character.Gender)
                .WriteUInt32((uint)character.Class);

            if (this.build == ClientBuild.TBC)
            {
                // response.declined.name[0].empty() ????
                this.Writer.WriteUInt8(0);
            }

            return this.Writer.Build();
        }
    }
}
