using System;
using System.Collections.Concurrent;
using Classic.Data.CharacterEnums;

namespace Classic.Data
{
    internal class Util
    {
        private static readonly Random Rand = new Random();

        public static ulong GenerateRandUlong()
        {
            var thirtyBits = (uint)Rand.Next(1 << 30);
            var twoBits = (uint)Rand.Next(1 << 2);

            return (thirtyBits << 2) | twoBits;
        }
    }

    public class Character
    {
        public Character()
        {
            this.ID = Util.GenerateRandUlong();
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
    }
}
