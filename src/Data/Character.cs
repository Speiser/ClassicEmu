using System.Collections.Concurrent;
using Classic.Cryptography;
using Classic.Data.CharacterEnums;

namespace Classic.Data
{
    public class Character
    {
        public Character()
        {
            this.ID = Random.GetUInt64();
        }

        public ulong ID { get; }
        public string Name { get; set; }
        public byte Level { get; set; }
        public Race Race { get; set; }
        public Classes Class { get; set; }
        public Gender Gender { get; set; }
        public byte Skin { get; set; }
        public byte Face { get; set; }
        public byte HairStyle { get; set; }
        public byte HairColor { get; set; }
        public byte FacialHair { get; set; }
        public byte OutfitId { get; set; }
        public Map Map { get; set; }
        public ConcurrentBag<InventoryItem> Inventory { get; } = new ConcurrentBag<InventoryItem>();
        public CharacterStats Stats { get; set; } = new CharacterStats();
        public CharacterFlag Flag { get; set; } = CharacterFlag.None;
    }
}
