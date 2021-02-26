using System;
using Classic.Common;
using Classic.Data;
using Classic.World.Entities;
using Classic.World.Entities.Enums;
using Classic.World.Entities.Utils;
using Classic.World.Extensions;

namespace Classic.World.Messages.Server
{
    public class SMSG_UPDATE_OBJECT : ServerMessageBase<Opcode>
    {
        public SMSG_UPDATE_OBJECT() : base(Opcode.SMSG_UPDATE_OBJECT) { }

        public static SMSG_UPDATE_OBJECT CreateOwnPlayerObject(Character character, int build, out PlayerEntity player)
        {
            var update = new SMSG_UPDATE_OBJECT();

            update.Writer
                .WriteUInt32(1) // blocks.Count
                .WriteUInt8(0) // hasTransport

                .WriteUInt8((byte)ObjectUpdateType.UPDATETYPE_CREATE_OBJECT_SELF)
                .WriteBytes(character.ID.ToPackedUInt64())

                .WriteUInt8((byte)TypeId.TypeidPlayer);

            var updateFlags = build switch
            {
                ClientBuild.Vanilla => (byte)(ObjectUpdateFlag_VANILLA.All
                                       | ObjectUpdateFlag_VANILLA.HasPosition
                                       | ObjectUpdateFlag_VANILLA.Living
                                       | ObjectUpdateFlag_VANILLA.Self),
                ClientBuild.TBC => (byte)(ObjectUpdateFlag_TBC.Highguid
                                   | ObjectUpdateFlag_TBC.HasPosition
                                   | ObjectUpdateFlag_TBC.Living
                                   | ObjectUpdateFlag_TBC.Self),
                _ => throw new NotImplementedException($"GetUpdateFlags(build: {build})"),
            };

            update.Writer
                .WriteUInt8(updateFlags)
                .WriteUInt32((uint)MovementFlags.None);

            if (build == ClientBuild.TBC) update.Writer.WriteUInt8(0); // moveFlags2

            update.Writer
                .WriteUInt32((uint)Environment.TickCount)
                .WriteMap(character.Position)

                .WriteFloat(0) // ??

                .WriteFloat(2.5f) // WalkSpeed
                .WriteFloat(7f) // RunSpeed
                .WriteFloat(2.5f) // Backwards WalkSpeed
                .WriteFloat(4.72f) // SwimSpeed
                .WriteFloat(2.5f); // Backwards SwimSpeed

            if (build == ClientBuild.TBC)
            {
                update.Writer
                    .WriteFloat(14f) // MOVE_FLIGHT
                    .WriteFloat(14f); // MOVE_FLIGHT_BACK
            }

            update.Writer
                .WriteFloat(3.14f) // TurnSpeed
                .WriteUInt32(0); // ??

            player = new PlayerEntity(character, build)
            {
                ObjectGuid = new ObjectGuid(character.ID),
                Guid = character.ID
            };

            player.WriteUpdateFields(update.Writer);
            return update;
        }

        public override byte[] Get() => this.Writer.Build();
    }

    public class SMSG_UPDATE_OBJECT_VANILLA : ServerMessageBase<Opcode>
    {
        public SMSG_UPDATE_OBJECT_VANILLA() : base(Opcode.SMSG_UPDATE_OBJECT) { }

        public static SMSG_UPDATE_OBJECT_VANILLA CreatePlayer(Character character)
        {
            var update = new SMSG_UPDATE_OBJECT_VANILLA();

            update.Writer
                .WriteUInt32(1) // blocks.Count
                .WriteUInt8(0) // hasTransport

                .WriteUInt8((byte)ObjectUpdateType.UPDATETYPE_CREATE_OBJECT_SELF)
                .WriteBytes(character.ID.ToPackedUInt64()) // ? 

                .WriteUInt8((byte)TypeId.TypeidPlayer)
                .WriteUInt8((byte)(ObjectUpdateFlag_VANILLA.All |
                                    ObjectUpdateFlag_VANILLA.HasPosition |
                                    ObjectUpdateFlag_VANILLA.Living))

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
            var player = new PlayerEntity(character, ClientBuild.Vanilla)
            {
                ObjectGuid = new ObjectGuid(character.ID),
                Guid = character.ID
            };

            player.WriteUpdateFields(update.Writer);
            return update;
        }

        public static SMSG_UPDATE_OBJECT_VANILLA CreateUnit(Creature unit)
        {
            var update = new SMSG_UPDATE_OBJECT_VANILLA();
            var entity = new UnitEntity(unit, ClientBuild.Vanilla)
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
                .WriteUInt8((byte)(ObjectUpdateFlag_VANILLA.All |
                                    ObjectUpdateFlag_VANILLA.HasPosition |
                                    ObjectUpdateFlag_VANILLA.Living))
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

        // UNUSED FOR NOW
        public static SMSG_UPDATE_OBJECT_VANILLA UpdateValues(PlayerEntity player)
        {
            var update = new SMSG_UPDATE_OBJECT_VANILLA();

            update.Writer
                .WriteUInt32(1) // blocks.Count
                .WriteUInt8(0) // hasTransport

                .WriteUInt8((byte)ObjectUpdateType.UPDATETYPE_VALUES)
                .WriteBytes(player.Character.ID.ToPackedUInt64());

            player.WriteUpdateFields(update.Writer);
            return update;
        }

        public override byte[] Get() => this.Writer.Build();
    }
}