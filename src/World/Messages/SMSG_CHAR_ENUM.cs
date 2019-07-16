using Classic.Common;
using Classic.Data;
using Classic.Data.CharacterEnums;
using System.Collections.Generic;
using System.Linq;

namespace Classic.World.Messages
{
    public class SMSG_CHAR_ENUM : ServerMessageBase<Opcode>
    {
        private readonly IEnumerable<Character> characters;
        public SMSG_CHAR_ENUM(IEnumerable<Character> characters) : base(Opcode.SMSG_CHAR_ENUM)
        {
            this.characters = characters;
        }

        public override byte[] Get()
        {
            this.Writer.WriteUInt8((byte)this.characters.Count());

            foreach (var c in this.characters)
            {
                this.Writer
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
                    .WriteUInt8(c.Level) // Character Level
                    .WriteInt32(0) // Map zone
                    .WriteInt32(0) // Map id
                    .WriteFloat(0f) // X coord
                    .WriteFloat(0f) // Y coord
                    .WriteFloat(0f) // Z coord
                    .WriteInt32(0) // Guild id
                    .WriteInt32(1) // unk?
                    .WriteUInt8(3) // Reststate
                    .WriteInt32(0) // Pet model
                    .WriteInt32(0) // Pet level
                    .WriteInt32(0); // Pet family

                // Inventory
                for (byte i = 0; i < 20; i++)
                {
                    var item = c.Inventory.FirstOrDefault(x => x.ItemSlot == i.AsEnum<ItemSlot>());

                    this.Writer.WriteInt32(item?.DisplayID ?? 0); // Display ID
                    this.Writer.WriteUInt8(i); // Inventory type
                }
            }

            return this.Writer.Build();
        }
    }
}
