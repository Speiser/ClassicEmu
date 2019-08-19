using System;
using System.Collections.Concurrent;
using Classic.Data.Enums.Character;

namespace Classic.Data
{
    public class Character
    {
        public Character()
        {
            ID = Cryptography.Random.GetUInt64();
            Created = DateTime.UtcNow;
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
        public Map Position { get; set; }
        public ConcurrentBag<InventoryItem> Inventory { get; } = new ConcurrentBag<InventoryItem>();
        public CharacterStats Stats { get; } = new CharacterStats();
        public CharacterFlag Flag { get; set; } = CharacterFlag.None;
        public DateTime Created { get; }
    }
}
