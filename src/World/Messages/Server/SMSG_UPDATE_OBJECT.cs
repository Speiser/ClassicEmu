using System;
using Classic.Common;
using Classic.Data;
using Classic.Data.Enums.Character;
using Classic.World.Entities;
using Classic.World.Entities.Enums;
using Classic.World.Entities.Utils;
using Classic.World.Extensions;

namespace Classic.World.Messages.Server
{
    public class SMSG_UPDATE_OBJECT : ServerMessageBase<Opcode>
    {
        public SMSG_UPDATE_OBJECT() : base(Opcode.SMSG_UPDATE_OBJECT) { }

        public static SMSG_UPDATE_OBJECT CreateOwnPlayerObject(Character character, out PlayerEntity player)
        {
            var update = new SMSG_UPDATE_OBJECT();

            update.Writer
                .WriteUInt32(1) // blocks.Count
                .WriteUInt8(0) // hasTransport

                .WriteUInt8((byte) ObjectUpdateType.UPDATETYPE_CREATE_OBJECT_SELF)
                .WriteBytes(character.ID.ToPackedUInt64())

                .WriteUInt8((byte) TypeId.TypeidPlayer)
                .WriteUInt8((byte) (ObjectUpdateFlag.All |
                                    ObjectUpdateFlag.HasPosition |
                                    ObjectUpdateFlag.Living |
                                    ObjectUpdateFlag.Self))

                .WriteUInt32((uint) MovementFlags.None)
                .WriteUInt32((uint) Environment.TickCount)

                .WriteMap(character.Position)

                .WriteFloat(0) // ??

                .WriteFloat(2.5f) // WalkSpeed
                .WriteFloat(7f) // RunSpeed
                .WriteFloat(2.5f) // Backwards WalkSpeed
                .WriteFloat(4.72f) // SwimSpeed
                .WriteFloat(2.5f) // Backwards SwimSpeed
                .WriteFloat(3.14f) // TurnSpeed

                .WriteInt32(1); // ??

            player = new PlayerEntity(character)
            {
                ObjectGuid = new ObjectGuid(character.ID),
                Guid = character.ID
            };

            player.WriteUpdateFields(update.Writer);
            return update;
        }

        public static SMSG_UPDATE_OBJECT CreatePlayer(Character character)
        {
            var update = new SMSG_UPDATE_OBJECT();

            update.Writer
                .WriteUInt32(1) // blocks.Count
                .WriteUInt8(0) // hasTransport

                .WriteUInt8((byte)ObjectUpdateType.UPDATETYPE_CREATE_OBJECT_SELF)
                .WriteBytes(character.ID.ToPackedUInt64()) // ? 

                .WriteUInt8((byte)TypeId.TypeidPlayer)
                .WriteUInt8((byte)(ObjectUpdateFlag.All |
                                    ObjectUpdateFlag.HasPosition |
                                    ObjectUpdateFlag.Living))

                .WriteUInt32((uint)MovementFlags.None)
                .WriteUInt32((uint)Environment.TickCount)

                .WriteMap(character.Position)

                .WriteFloat(0) // ??

                .WriteFloat(2.5f) // WalkSpeed
                .WriteFloat(7f) // RunSpeed
                .WriteFloat(2.5f) // Backwards WalkSpeed
                .WriteFloat(4.72f) // SwimSpeed
                .WriteFloat(2.5f) // Backwards SwimSpeed
                .WriteFloat(3.14f) // TurnSpeed

                .WriteInt32(1); // ??

            // TODO: Can be done somewhere else?
            var player = new PlayerEntity(character)
            {
                ObjectGuid = new ObjectGuid(character.ID),
                Guid = character.ID
            };

            player.WriteUpdateFields(update.Writer);
            return update;
        }

        public static SMSG_UPDATE_OBJECT CreateUnit(Creature unit)
        {
            var update = new SMSG_UPDATE_OBJECT();
            var entity = new UnitEntity(unit)
            {
                ObjectGuid = new ObjectGuid(unit.ID),
                Guid = unit.ID
            };

            update.Writer
                .WriteUInt32(1) // blocks.Count
                .WriteUInt8(0) // hasTransport

                .WriteUInt8((byte)ObjectUpdateType.UPDATETYPE_CREATE_OBJECT)
                .WriteBytes(unit.ID.ToPackedUInt64())
                .WriteUInt8((byte)TypeId.TypeidUnit)
                .WriteUInt8((byte)(ObjectUpdateFlag.All |
                                    ObjectUpdateFlag.HasPosition |
                                    ObjectUpdateFlag.Living))
                .WriteUInt32((uint)MovementFlags.None)
                .WriteUInt32((uint)Environment.TickCount)
                .WriteMap(unit.Position)

                .WriteFloat(0) // ??

                .WriteFloat(2.5f) // WalkSpeed
                .WriteFloat(7f) // RunSpeed
                .WriteFloat(4.5f) // Backwards WalkSpeed
                .WriteFloat(4.72f) // SwimSpeed
                .WriteFloat(2.5f) // Backwards SwimSpeed
                .WriteFloat(3.14f) // TurnSpeed

                .WriteInt32(0); // ??

            entity.WriteUpdateFields(update.Writer);
            return update;
        }

        public override byte[] Get() => this.Writer.Build();
    }
}