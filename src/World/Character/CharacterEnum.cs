using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classic.Common;
using Classic.Data;
using Classic.Data.CharacterEnums;

namespace Classic.World.Character
{
    public class CharacterEnum
    {
#if DEBUG
        public const bool ADD_DEFAULT_ITEMS = true;
#endif

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
                        .WriteUInt8(60) // Character Level
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


                    // Sulf 29698
#if DEBUG
                    if (ADD_DEFAULT_ITEMS)
                    {
                        c.Inventory.Add(new InventoryItem { DisplayID = 30316, ItemSlot = ItemSlot.Head });
                        c.Inventory.Add(new InventoryItem { DisplayID = 30318, ItemSlot = ItemSlot.Shoulder });
                        c.Inventory.Add(new InventoryItem { DisplayID = 30315, ItemSlot = ItemSlot.Chest });
                        c.Inventory.Add(new InventoryItem { DisplayID = 30317, ItemSlot = ItemSlot.Leg });
                        c.Inventory.Add(new InventoryItem { DisplayID = 30319, ItemSlot = ItemSlot.Boots });
                        c.Inventory.Add(new InventoryItem { DisplayID = 30321, ItemSlot = ItemSlot.Gloves });
                    }
#endif

                    // Inventory
                    for (byte i = 0; i < 20; i++)
                    {
                        var item = c.Inventory.FirstOrDefault(x => x.ItemSlot == i.AsEnum<ItemSlot>());

                        packet.WriteInt32(item?.DisplayID ?? 0); // Display ID
                        packet.WriteUInt8(i); // Inventory type
                    }
                }

                return packet.Build();
            }
        }
    }
}
