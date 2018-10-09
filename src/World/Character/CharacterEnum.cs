using System;
using System.Collections.Generic;
using System.Text;
using Classic.Common;
using Classic.Data;

namespace Classic.World.Character
{
    public class CharacterEnum
    {
        public byte[] GetEmpty()
        {
            return new PacketWriter().WriteUInt8(0).Build();
        }

        public byte[] GetCharacters(User user)
        {
            using (var packet = new PacketWriter())
            {
                packet.WriteUInt8((byte)user.Characters.Count);

                foreach (var c in user.Characters)
                {
                    packet
                        .WriteUInt64(c.ID)
                        .WriteString(c.Name)
                        .WriteUInt8((byte)c.Race)
                        .WriteUInt8((byte)c.Class)
                        .WriteUInt8((byte)c.Gender)
                        .WriteUInt8(c.Skin)
                        .WriteUInt8(c.Face)
                        .WriteUInt8(c.HairStyle)
                        .WriteUInt8(c.HairColor)
                        .WriteUInt8(c.FacialHair)
                        .WriteUInt8(1) // Character Level
                        .WriteInt32(0) // Map zone
                        .WriteInt32(0) // Map id
                        .WriteFloat(0f) // X coord
                        .WriteFloat(0f) // Y coord
                        .WriteFloat(0f) // Z coord
                        .WriteInt32(0) // Guild id
                        .WriteInt32(1) // unk?
                        .WriteUInt8(0) // Reststate
                        .WriteInt32(0) // Pet model
                        .WriteInt32(0) // Pet level
                        .WriteInt32(0); // Pet family

                    // Inventory
                    for (var i = 0; i < 20; i++)
                    {
                        packet.WriteInt32(0); // Display id
                        packet.WriteUInt8(0); // Inventory type
                    }
                }

                return packet.Build();
            }
        }
    }
}
