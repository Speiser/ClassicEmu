using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Classic.World.Data.Enums.Character;

namespace Classic.World.Data
{
    public class Character : IHasPosition
    {
        public Character()
        {
            Id = Shared.Cryptography.Random.GetUInt64();
            Created = DateTime.UtcNow;
        }

        public ulong Id { get; set; }
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
        public CharacterAttributes Attributes { get; } = new CharacterAttributes(); // TODO: From ctor?
        public CharacterResistances Resistances { get; } = new CharacterResistances(); // TODO: From ctor?
        public CharacterStats Stats { get; } = new CharacterStats(); // TODO: From ctor?
        public CharacterStats MaxStats { get; } = new CharacterStats(); // TODO: From ctor?
        public CharacterFlag Flag { get; set; } = CharacterFlag.None;
        public DateTime Created { get; }
        public List<Skill> Skills { get; init; }
        public List<Spell> Spells { get; init; }
        public ActionBarItem[] ActionBar { get; init; }

        public int WatchFaction { get; set; } = 255; // ?? as enum
        public RestedState RestedState { get; set; } = RestedState.Rested;
        public StandState StandState { get; set; }
    }
}
