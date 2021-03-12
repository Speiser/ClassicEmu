using System;
using Classic.Shared.Data;
using Classic.World.Data;
using Classic.World.Extensions;

namespace Classic.World.Messages
{
    public class MovementUpdate : ServerMessageBase<Opcode>
    {
        private readonly Character character;
        private readonly MSG_MOVE_GENERIC original;
        private readonly int build;

        public MovementUpdate(Character character, MSG_MOVE_GENERIC original, Opcode opcode, int build) : base(opcode)
        {
            this.character = character;
            this.original = original;
            this.build = build;
        }

        public override byte[] Get()
        {
            this.Writer
                .WriteBytes(this.character.Id.ToPackedUInt64())
                .WriteUInt32((uint)this.original.MovementFlags)
                .WriteUInt32((uint)Environment.TickCount);

            if (this.build == ClientBuild.TBC) this.Writer.WriteUInt8(0); // Movementflags2

            return this.Writer
                .WriteFloat(this.original.MapX)
                .WriteFloat(this.original.MapY)
                .WriteFloat(this.original.MapZ)
                .WriteFloat(this.original.MapO)
                .Build();
        }
    }
}
