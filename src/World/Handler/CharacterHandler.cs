using System.Linq;
using Classic.Common;
using Classic.Data;
using Classic.Data.CharacterEnums;
using static Classic.World.Opcode;
using DCharacter = Classic.Data.Character;

namespace Classic.World.Handler
{
    public class CharacterHandler
    {
        [OpcodeHandler(CMSG_CHAR_ENUM)]
        public static void OnCharacterEnum(WorldClient client, byte[] data)
        {
#if DEBUG
            const bool ADD_DEFAULT_ITEMS = false;
#endif
            using (var packet = new PacketWriter())
            {
                packet.WriteUInt8((byte)client.User.Characters.Count);

                foreach (var c in client.User.Characters)
                {
                    packet
                        .WriteUInt64(c.ID)
                        .WriteString(c.Name)
                        .WriteUInt8((byte) c.Race)
                        .WriteUInt8((byte) c.Class)
                        .WriteUInt8((byte) c.Gender)
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


                    // Sulf 29698
#if DEBUG
                    if (ADD_DEFAULT_ITEMS)
                    {
                        c.Inventory.Add(new InventoryItem {DisplayID = 30316, ItemSlot = ItemSlot.Head});
                        c.Inventory.Add(new InventoryItem {DisplayID = 30318, ItemSlot = ItemSlot.Shoulder});
                        c.Inventory.Add(new InventoryItem {DisplayID = 30315, ItemSlot = ItemSlot.Chest});
                        c.Inventory.Add(new InventoryItem {DisplayID = 30317, ItemSlot = ItemSlot.Leg});
                        c.Inventory.Add(new InventoryItem {DisplayID = 30319, ItemSlot = ItemSlot.Boots});
                        c.Inventory.Add(new InventoryItem {DisplayID = 30321, ItemSlot = ItemSlot.Gloves});
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

                client.SendPacket(packet.Build(), Opcode.SMSG_CHAR_ENUM);
            }
        }

        [OpcodeHandler(CMSG_CHAR_CREATE)]
        public static void OnCharacterCreate(WorldClient client, byte[] data)
        {
            using (var reader = new PacketReader(data))
            {
                // TODO: Maybe evaluate later: len, cmd ??
                reader.Skip(6);

                var character = new DCharacter
                {
                    Name = reader.ReadString(),
                    Race = reader.ReadByte().AsEnum<Race>(),
                    Class = reader.ReadByte().AsEnum<Classes>(),
                    Gender = reader.ReadByte().AsEnum<Gender>(),
                    Skin = reader.ReadByte(),
                    Face = reader.ReadByte(),
                    HairStyle = reader.ReadByte(),
                    HairColor = reader.ReadByte(),
                    FacialHair = reader.ReadByte(),
                    OutfitId = reader.ReadByte()
                };

                client.User.Characters.Add(character);
            }

            using (var writer = new PacketWriter())
            {
                // TODO: Always returns success atm
                client.SendPacket(writer.WriteUInt8(46).Build(), Opcode.SMSG_CHAR_CREATE);
            }
        }
    }
}
