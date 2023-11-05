using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Classic.World.Data.Enums.Character;
using Classic.World.Data.Enums.Map;

namespace Classic.World.Data;

public class Character : IHasPosition
{
    public Character()
    {
        Id = Shared.Cryptography.Random.GetUInt64();
        Created = DateTime.UtcNow;
    }

    public Character(PCharacter c)
    {
        this.AccountId = c.AccountId;
        this.Id = (ulong)c.Id;
        this.Name = c.Name;
        this.Level = c.Level;
        this.Race = (Race)c.Race;
        this.Class = (Classes)c.Class;
        this.Gender = (Gender)c.Gender;
        this.Skin = c.Skin;
        this.Face = c.Face;
        this.HairStyle = c.HairStyle;
        this.HairColor = c.HairColor;
        this.FacialHair = c.FacialHair;
        this.OutfitId = c.OutfitId;
        this.Position = new Map
        {
            ID = (MapID)c.MapId,
            Zone = (ZoneID)c.ZoneId,
            Orientation = c.PositionO,
            X = c.PositionX,
            Y = c.PositionY,
            Z = c.PositionZ,
        };
        this.Flag = (CharacterFlag)c.Flag;
        this.Created = c.Created;
        this.Skills = CharacterFactory.GetInitialSkills(this.Race, this.Class);
        this.Spells = CharacterFactory.GetInitialSpells(this.Race, this.Class);
        this.ActionBar = CharacterFactory.GetInitialActionBar(this.Race, this.Class);
        this.WatchFaction = c.WatchFaction;
        this.RestedState = (RestedState)c.RestedState;
        this.StandState = (StandState)c.StandState;
    }

    public int AccountId { get; set; }
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

public class PCharacter
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public string Name { get; set; }
    public byte Level { get; set; }
    public int Xp { get; set; }
    public short Race { get; set; }
    public short Class { get; set; }
    public short Gender { get; set; }
    public byte Skin { get; set; }
    public byte Face { get; set; }
    public byte HairStyle { get; set; }
    public byte HairColor { get; set; }
    public byte FacialHair { get; set; }
    public byte OutfitId { get; set; }
    public DateTime Created { get; set; }
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float PositionZ { get; set; }
    public float PositionO { get; set; }
    public int MapId { get; set; }
    public int ZoneId { get; set; }
    public int Flag { get; set; }
    public int WatchFaction { get; set; }
    public byte RestedState { get; set; }
    public byte StandState { get; set; }
}
