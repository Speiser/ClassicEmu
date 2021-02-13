using System;
using Classic.Common;
using Classic.Data;
using Classic.World.Extensions;

namespace Classic.World.Messages
{
    public class MovementUpdate : ServerMessageBase<Opcode>
    {
        private readonly Character character;
        private readonly MSG_MOVE_GENERIC original;

        public MovementUpdate(Character character, MSG_MOVE_GENERIC original, Opcode opcode) : base(opcode)
        {
            this.character = character;
            this.original = original;
        }

        public override byte[] Get() => this.Writer
            .WriteBytes(this.character.ID.ToPackedUInt64())
            .WriteUInt32((uint)this.original.MovementFlags)
            .WriteUInt32((uint)Environment.TickCount)
            .WriteFloat(this.original.MapX)
            .WriteFloat(this.original.MapY)
            .WriteFloat(this.original.MapZ)
            .WriteFloat(this.original.MapO)
            .Build();
    }
}
