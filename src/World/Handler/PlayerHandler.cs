using System;
using System.IO;
using System.Linq;
using Classic.Common;
using static Classic.World.Opcode;

namespace Classic.World.Handler
{
    public static class PlayerHandler
    {
        [OpcodeHandler(CMSG_PLAYER_LOGIN)]
        public static void OnPlayerLogin(WorldClient client, byte[] data)
        {
            uint charId = 0;

            using (var reader = new PacketReader(data))
            {
                reader.Skip(6);
                charId = reader.ReadUInt32();
            }

            var character = client.User.Characters.Single(x => x.ID == charId);

            client.Log($"Player logged in with char {character.Name}");


            // SMSG_LOGIN_VERIFY_WORLD
            using (var writer = new PacketWriter())
            {
                // 0 -8919.284180 -117.894028 82.339821 = human starting area
                writer.WriteInt32(0); // MapID
                writer.WriteFloat(-8919.284180F); // MapX
                writer.WriteFloat(-117.894028F); // MapY
                writer.WriteFloat(82.339821F); // MapZ
                writer.WriteFloat(1F); // MapO (Orientation)
                client.SendPacket(writer.Build(), SMSG_LOGIN_VERIFY_WORLD);
            }

            // SMSG_ACCOUNT_DATA_TIMES
            using (var writer = new PacketWriter())
            {
                writer.WriteBytes(new byte[80]);
                client.SendPacket(writer.Build(), SMSG_ACCOUNT_DATA_TIMES);
            }

            // TODO: Server messages??

            // SMSG_SET_REST_START
            using (var writer = new PacketWriter())
            {
                writer.WriteUInt32(1000);
                client.SendPacket(writer.Build(), SMSG_SET_REST_START);
            }

            // SMSG_BINDPOINTUPDATE - HEARTHSTONE????
            using (var writer = new PacketWriter())
            {
                writer.WriteFloat(-8919.284180F); // MapX
                writer.WriteFloat(-117.894028F); // MapY
                writer.WriteFloat(82.339821F); // MapZ
                writer.WriteUInt32(0); // MapID
                writer.WriteUInt32(12); // ZoneID
                client.SendPacket(writer.Build(), SMSG_BINDPOINTUPDATE);
            }

            // SMSG_TUTORIAL_FLAGS
            using (var writer = new PacketWriter())
            {
                for (var i = 0; i < 32; i++)
                {
                    writer.WriteUInt8(0xFF);
                }

                client.SendPacket(writer.Build(), SMSG_TUTORIAL_FLAGS);
            }

            // SMSG_LOGIN_SETTIMESPEED
            using (var writer = new PacketWriter())
            {
                writer.WriteUInt32(Convert.ToUInt32((DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds)); // TIME
                writer.WriteFloat(0.01666667f); // Speed

                client.SendPacket(writer.Build(), SMSG_LOGIN_SETTIMESPEED);
            }

            // TODO: SMSG_INITIAL_SPELLS
            // TODO: SMSG_ACTION_BUTTONS
            // TODO: SMSG_INITIALIZE_FACTIONS
            // TODO: SMSG_TRIGGER_CINEMATIC

            // SMSG_CORPSE_RECLAIM_DELAY
            using (var writer = new PacketWriter())
            {
                writer.WriteInt32(2000);

                client.SendPacket(writer.Build(), SMSG_CORPSE_RECLAIM_DELAY);
            }

            // SMSG_INIT_WORLD_STATES
            // https://www.ownedcore.com/forums/world-of-warcraft/world-of-warcraft-emulator-servers/wow-emu-questions-requests/327009-making-capturable-pvp-zones.html
            using (var writer = new PacketWriter())
            {
                writer.WriteUInt64(0); // MapID
                writer.WriteUInt32(12); // MapZone (ZoneID??)
                writer.WriteUInt32(0); // AreaID
                writer.WriteUInt16(12); // count of uint64 blocks

                writer.WriteUInt64(0x8d8);
                writer.WriteUInt64(0x0);
                writer.WriteUInt64(0x8d7);
                writer.WriteUInt64(0x0);
                writer.WriteUInt64(0x8d6);
                writer.WriteUInt64(0x0);
                writer.WriteUInt64(0x8d5);
                writer.WriteUInt64(0x0);
                writer.WriteUInt64(0x8d4);
                writer.WriteUInt64(0x0);
                writer.WriteUInt64(0x8d3);
                writer.WriteUInt64(0x0);

                client.SendPacket(writer.Build(), SMSG_INIT_WORLD_STATES);
            }

            // SMSG_UPDATE_OBJECT
            using (var writer = new PacketWriter())
            {
                writer.WriteUInt8(3); // UPDATETYPE_CREATE_OBJECT_SELF
                writer.WriteBytes(ToPackedUInt64(character.ID));
                writer.WriteUInt8(4); // TypeidPlayer
                writer.WriteUInt8(0x0010 | 0x0040 | 0x0020 | 0x0001); // Update flag
                writer.WriteUInt32(0x00000000); // Movement flag
                writer.WriteUInt32((uint)Environment.TickCount);

                writer.WriteFloat(-8919.284180F); // MapX
                writer.WriteFloat(-117.894028F); // MapY
                writer.WriteFloat(82.339821F); // MapZ
                writer.WriteFloat(1F); // MapO (Orientation)

                writer.WriteFloat(0); // ???
                writer.WriteFloat(2.5f); // WalkSpeed
                writer.WriteFloat(7f * 1); // RunSpeed
                writer.WriteFloat(2.5f); // Backwards WalkSpeed
                writer.WriteFloat(4.7222f); // SwimSpeed
                writer.WriteFloat(2.5f); // Backwards SwimSpeed
                writer.WriteFloat(3.14f); // TurnSpeed

                writer.WriteInt32(0x1); // ???

                writer.WriteBytes(
                    // https://www.ownedcore.com/forums/world-of-warcraft/world-of-warcraft-emulator-servers/wow-emu-questions-requests/390468-smsg_update_object-documentation.html
                    // ^ TODO: also contains description of all fields
                    0x2a, // Mask Size ((1326 + 31) / 32 = 42)

                    // Values Update Mask
                    0b00010111, 0x00, 0x80, 0x01, 0x01, 0x00, 0b11000000, 0x00, 0x18, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                    // Update Values:
                    0x01, 0x02, 0x03, 0x00,  // OBJECT_FIELD_GUID Low GUID [Required]
                    0x00, 0x00, 0x00, 0x00,  // OBJECT_FIELD_GUID High GUID [Required]
                    0x19, 0x00, 0x00, 0x00,  // OBJECT_FIELD_TYPE -> unit | player | object
                    0x00, 0x00, 0x80, 0x3f,  // OBJECT_FIELD_SCALE_X 
                    0x01, 0x01, 0x01, 0x01,  // UNIT_FIELD_BYTES_0 Race(Human), Class(Warrior), Gender(Female), PowerType(Rage)
                    0x3c, 0x00, 0x00, 0x00,  // UNIT_FIELD_HEALTH 
                    0x3c, 0x00, 0x00, 0x00,  // UNIT_FIELD_MAXHEALTH
                    0x01, 0x00, 0x00, 0x00,  // UNIT_FIELD_LEVEL
                    0x01, 0x00, 0x00, 0x00,  // UNIT_FIELD_FACTIONTEMPLATE [Required]
                    0x0c, 0x4d, 0x00, 0x00,  // UNIT_FIELD_DISPLAYID (Human Female = 19724) [Required]
                    0x0c, 0x4d, 0x00, 0x00); // UNIT_FIELD_NATIVEDISPLAYID (Human Female = 19724) [Required])

                client.SendPacket(writer.Build(), SMSG_UPDATE_OBJECT);
            }
        }

        // https://github.com/andrewmunro/Vanilla/blob/f0f8ad5f833f299cf746c200dba143c530240c35/Vanilla/Vanilla.Core/Extensions/BinaryWriterExtension.cs
        private static byte[] ToPackedUInt64(ulong number)
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            {
                var buffer = BitConverter.GetBytes(number);

                byte mask = 0;
                var startPos = writer.BaseStream.Position;

                writer.Write(mask);

                for (var i = 0; i < 8; i++)
                {
                    if (buffer[i] != 0)
                    {
                        mask |= (byte)(1 << i);
                        writer.Write(buffer[i]);
                    }
                }

                var endPos = writer.BaseStream.Position;

                writer.BaseStream.Position = startPos;
                writer.Write(mask);
                writer.BaseStream.Position = endPos;

                return ms.ToArray();
            }
        }
    }
}
