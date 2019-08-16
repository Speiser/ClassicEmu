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
        public Stats Stats { get; set; } = new Stats();
        public CharacterFlag Flag { get; set; } = CharacterFlag.None;
    }

    public enum CharacterFlag
    {
        None = 0x0,

        /// <summary>
        ///     Character Locked for Paid Character Transfer
        /// </summary>
        LockedForTransfer = 0x4,

        HideHelm = 0x400,

        HideCloak = 0x800,

        /// <summary>
        ///     Player is ghost in char selection screen
        /// </summary>
        Ghost = 0x2000,

        /// <summary>
        ///     On login player will be asked to change name
        /// </summary>
        Rename = 0x4000,

        LockedByBilling = 0x1000000,

        Declined = 0x2000000
    }

    public class Stats
    {
        public int Life { get; set; } = 100;
        public int Mana { get; set; } = 100;
        public int Rage { get; set; } = 0;
        public int Energy { get; set; } = 0;
    }
}
