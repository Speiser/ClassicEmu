using System;
using Classic.Common;
using Classic.Data;

namespace Classic.World.Messages.Server
{
    public class SMSG_NAME_QUERY_RESPONSE : ServerMessageBase<Opcode>
    {
        private readonly Character character;

        public SMSG_NAME_QUERY_RESPONSE(Character character) : base(Opcode.SMSG_NAME_QUERY_RESPONSE)
        {
            this.character = character;
        }

        public override byte[] Get() => this.Writer
            .WriteUInt64(character.ID)
            .WriteString(character.Name)
            .WriteUInt8(0)
            .WriteUInt32((uint)character.Race)
            .WriteUInt32((uint)character.Gender)
            .WriteUInt32((uint)character.Class)
            .Build();
    }
}
