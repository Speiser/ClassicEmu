using System.Collections.Generic;
using System.Linq;
using Classic.World.Data;
using Classic.World.Data.Enums.Character;
using Classic.World.Extensions;

namespace Classic.World.Messages.Server
{
    public class SMSG_CHAR_ENUM_VANILLA : ServerMessageBase<Opcode>
    {
        private readonly IEnumerable<Character> characters;
        public SMSG_CHAR_ENUM_VANILLA(IEnumerable<Character> characters) : base(Opcode.SMSG_CHAR_ENUM)
        {
            this.characters = characters;
        }

        public override byte[] Get()
        {
            this.Writer.WriteUInt8((byte)this.characters.Count());

            foreach (var c in this.characters.OrderBy(c => c.Created))
            {
                this.Writer
                    .WriteUInt64(c.Id)
                    .WriteString(c.Name)
                    .WriteUInt8((byte)c.Race)
                    .WriteUInt8((byte)c.Class)
                    .WriteUInt8((byte)c.Gender)
                    .WriteUInt8(c.Skin)
                    .WriteUInt8(c.Face)
                    .WriteUInt8(c.HairStyle)
                    .WriteUInt8(c.HairColor)
                    .WriteUInt8(c.FacialHair)
                    .WriteUInt8(c.Level)
                    .WriteInt32((int)c.Position.Zone)
                    .WriteInt32((int)c.Position.ID)
                    .WriteFloat(c.Position.X)
                    .WriteFloat(c.Position.Y)
                    .WriteFloat(c.Position.Z)
                    .WriteInt32(0) // Guild id
                    .WriteInt32((int) c.Flag)
                    .WriteUInt8((byte) c.RestedState)
                    .WriteInt32(0) // Pet model
                    .WriteInt32(0) // Pet level
                    .WriteInt32(0); // Pet family

                // Inventory
                for (byte i = 0; i < 20; i++)
                {
                    var item = c.Inventory.FirstOrDefault(x => x.ItemSlot == i.AsEnum<ItemSlot>());

                    this.Writer.WriteInt32(item?.DisplayID ?? 0);
                    this.Writer.WriteUInt8(i); // Inventory type
                }
            }

            return this.Writer.Build();
        }
    }

    public class SMSG_CHAR_ENUM_TBC : ServerMessageBase<Opcode>
    {
        private readonly IEnumerable<Character> characters;
        public SMSG_CHAR_ENUM_TBC(IEnumerable<Character> characters) : base(Opcode.SMSG_CHAR_ENUM)
        {
            this.characters = characters;
        }

        public override byte[] Get()
        {
            this.Writer.WriteUInt8((byte)this.characters.Count());

            foreach (var c in this.characters.OrderBy(c => c.Created))
            {
                this.Writer
                    .WriteUInt64(c.Id)
                    .WriteString(c.Name)
                    .WriteUInt8((byte)c.Race)
                    .WriteUInt8((byte)c.Class)
                    .WriteUInt8((byte)c.Gender)
                    .WriteUInt8(c.Skin)
                    .WriteUInt8(c.Face)
                    .WriteUInt8(c.HairStyle)
                    .WriteUInt8(c.HairColor)
                    .WriteUInt8(c.FacialHair)
                    .WriteUInt8(c.Level)
                    .WriteInt32((int)c.Position.Zone)
                    .WriteInt32((int)c.Position.ID)
                    .WriteFloat(c.Position.X)
                    .WriteFloat(c.Position.Y)
                    .WriteFloat(c.Position.Z)
                    .WriteInt32(0) // Guild id
                    .WriteUInt32((uint)c.Flag) // NEW int -> uint
                    .WriteUInt8(1) // NEW first login instead of rested state
                    .WriteUInt32(0) // Pet display id - NEW int -> uint
                    .WriteUInt32(0) // Pet level - NEW int -> uint
                    .WriteUInt32(0); // Pet family - NEW int -> uint

                // Inventory
                for (byte i = 0; i < 20; i++)
                {
                    var item = c.Inventory.FirstOrDefault(x => x.ItemSlot == i.AsEnum<ItemSlot>());

                    this.Writer.WriteInt32(item?.DisplayID ?? 0);
                    this.Writer.WriteUInt8(i); // Inventory type
                    this.Writer.WriteUInt32(0); // NEW enchant 
                }
            }

            return this.Writer.Build();
        }
    }
}
