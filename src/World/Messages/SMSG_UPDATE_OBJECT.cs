using Classic.Common;
using Classic.Data;
using Classic.Data.CharacterEnums;
using System;
using System.Collections;

namespace Classic.World.Messages
{
    public class SMSG_UPDATE_OBJECT : ServerMessageBase<Opcode>
    {
        public SMSG_UPDATE_OBJECT() : base(Opcode.SMSG_UPDATE_OBJECT) { }

        public static SMSG_UPDATE_OBJECT CreateOwnPlayerObject(Character character)
        {
            var update = new SMSG_UPDATE_OBJECT();

            update.Writer
                .WriteUInt32(1) // blocks.Count
                .WriteUInt8(0) // hasTransport

                .WriteUInt8((byte) ObjectUpdateType.UPDATETYPE_CREATE_OBJECT_SELF)
                .WriteBytes(character.ID.ToPackedUInt64()) // = update.Writer.WritePackedUInt64(character.Uid);

                .WriteUInt8((byte) TypeId.TypeidPlayer)
                .WriteUInt8((byte) (ObjectUpdateFlag.All |
                                    ObjectUpdateFlag.HasPosition |
                                    ObjectUpdateFlag.Living |
                                    ObjectUpdateFlag.Self))

                .WriteUInt32((uint) MovementFlags.None)
                .WriteUInt32((uint) Environment.TickCount)

                .WriteMap(character.Map)

                .WriteFloat(0) // ??

                .WriteFloat(2.5f) // WalkSpeed
                .WriteFloat(7f * 1) // RunSpeed
                .WriteFloat(2.5f) // Backwards WalkSpeed
                .WriteFloat(4.7222f) // SwimSpeed
                .WriteFloat(2.5f) // Backwards SwimSpeed
                .WriteFloat(3.14f) // TurnSpeed

                .WriteInt32(0x1); // ??

            var entity = new PlayerEntity(character)
            {
                ObjectGuid = new ObjectGuid(character.ID),
                Guid = character.ID
            };

            entity.WriteUpdateFields(update.Writer);
            return update;
        } 

        public override byte[] Get() => this.Writer.Build();
    }
    internal static class PacketWriterExtensions
    {
        public static PacketWriter WriteMap(this PacketWriter writer, Map map)
        {
            return writer
                .WriteFloat(map.X)
                .WriteFloat(map.Y)
                .WriteFloat(map.Z)
                .WriteFloat(map.Orientation);
        }
    }

    [Flags] internal enum MovementFlags
    {
        None = 0x00000000,
        Forward = 0x00000001,
        Backward = 0x00000002,
        StrafeLeft = 0x00000004,
        StrafeRight = 0x00000008,
        TurnLeft = 0x00000010,
        TurnRight = 0x00000020,
        PitchUp = 0x00000040,
        PitchDown = 0x00000080,
        WalkMode = 0x00000100, // Walking

        Levitating = 0x00000400,
        Root = 0x00000800, // [-ZERO] is it really need and correct value
        Falling = 0x00002000,
        Fallingfar = 0x00004000,
        Swimming = 0x00200000, // appears with fly flag also
        Ascending = 0x00400000, // [-ZERO] is it really need and correct value
        CanFly = 0x00800000, // [-ZERO] is it really need and correct value
        Flying = 0x01000000, // [-ZERO] is it really need and correct value

        Ontransport = 0x02000000, // Used for flying on some creatures
        SplineElevation = 0x04000000, // used for flight paths
        SplineEnabled = 0x08000000, // used for flight paths
        Waterwalking = 0x10000000, // prevent unit from falling through water
        SafeFall = 0x20000000, // active rogue safe fall spell (passive)
        Hover = 0x40000000
    }
    [Flags] internal enum ObjectUpdateFlag : byte
    {
        None = 0x0000,
        Self = 0x0001,
        Transport = 0x0002,
        Fullguid = 0x0004,
        Highguid = 0x0008,
        All = 0x0010,
        Living = 0x0020,
        HasPosition = 0x0040
    }
    internal enum ObjectUpdateType
    {
        UPDATETYPE_VALUES = 0,

        //  1 byte  - MASK
        //  8 bytes - GUID
        //  Goto Update Block
        UPDATETYPE_MOVEMENT = 1,

        //  1 byte  - MASK
        //  8 bytes - GUID
        //  Goto Position Update
        UPDATETYPE_CREATE_OBJECT = 2,
        UPDATETYPE_CREATE_OBJECT_SELF = 3,

        //  1 byte  - MASK
        //  8 bytes - GUID
        //  1 byte - Object Type (*)
        //  Goto Position Update
        //  Goto Update Block
        UPDATETYPE_OUT_OF_RANGE_OBJECTS = 4,

        //  4 bytes - Count
        //  Loop Count Times:
        //  1 byte  - MASK
        //  8 bytes - GUID
        UPDATETYPE_NEAR_OBJECTS = 5 // looks like 4 & 5 do the same thing
        //  4 bytes - Count
        //  Loop Count Times:
        //  1 byte  - MASK
        //  8 bytes - GUID
    }

    internal enum PlayerFields
    {
        PLAYER_DUEL_ARBITER = 0x00 + UnitFields.UNIT_END, // Size:2
        PLAYER_FLAGS = 0x02 + UnitFields.UNIT_END, // Size:1
        PLAYER_GUILDID = 0x03 + UnitFields.UNIT_END, // Size:1
        PLAYER_GUILDRANK = 0x04 + UnitFields.UNIT_END, // Size:1
        PLAYER_BYTES = 0x05 + UnitFields.UNIT_END, // Size:1
        PLAYER_BYTES_2 = 0x06 + UnitFields.UNIT_END, // Size:1
        PLAYER_BYTES_3 = 0x07 + UnitFields.UNIT_END, // Size:1
        PLAYER_DUEL_TEAM = 0x08 + UnitFields.UNIT_END, // Size:1
        PLAYER_GUILD_TIMESTAMP = 0x09 + UnitFields.UNIT_END, // Size:1
        PLAYER_QUEST_LOG_1_1 = 0x0A + UnitFields.UNIT_END, // count = 20
        PLAYER_QUEST_LOG_1_2 = 0x0B + UnitFields.UNIT_END,
        PLAYER_QUEST_LOG_1_3 = 0x0C + UnitFields.UNIT_END,
        PLAYER_QUEST_LOG_LAST_1 = 0x43 + UnitFields.UNIT_END,
        PLAYER_QUEST_LOG_LAST_2 = 0x44 + UnitFields.UNIT_END,
        PLAYER_QUEST_LOG_LAST_3 = 0x45 + UnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_1_CREATOR = 0x46 + UnitFields.UNIT_END, // Size:2, count = 19
        PLAYER_VISIBLE_ITEM_1_0 = 0x48 + UnitFields.UNIT_END, // Size:8
        PLAYER_VISIBLE_ITEM_1_PROPERTIES = 0x50 + UnitFields.UNIT_END, // Size:1
        PLAYER_VISIBLE_ITEM_1_PAD = 0x51 + UnitFields.UNIT_END, // Size:1
        PLAYER_VISIBLE_ITEM_LAST_CREATOR = 0x11e + UnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_LAST_0 = 0x120 + UnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_LAST_PROPERTIES = 0x128 + UnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_LAST_PAD = 0x129 + UnitFields.UNIT_END,
        PLAYER_FIELD_INV_SLOT_HEAD = 0x12a + UnitFields.UNIT_END, // Size:46
        PLAYER_FIELD_PACK_SLOT_1 = 0x158 + UnitFields.UNIT_END, // Size:32
        PLAYER_FIELD_PACK_SLOT_LAST = 0x176 + UnitFields.UNIT_END,
        PLAYER_FIELD_BANK_SLOT_1 = 0x178 + UnitFields.UNIT_END, // Size:48
        PLAYER_FIELD_BANK_SLOT_LAST = 0x1a6 + UnitFields.UNIT_END,
        PLAYER_FIELD_BANKBAG_SLOT_1 = 0x1a8 + UnitFields.UNIT_END, // Size:12
        PLAYER_FIELD_BANKBAG_SLOT_LAST = 0xab2 + UnitFields.UNIT_END,
        PLAYER_FIELD_VENDORBUYBACK_SLOT_1 = 0x1b4 + UnitFields.UNIT_END, // Size:24
        PLAYER_FIELD_VENDORBUYBACK_SLOT_LAST = 0x1ca + UnitFields.UNIT_END,
        PLAYER_FIELD_KEYRING_SLOT_1 = 0x1cc + UnitFields.UNIT_END, // Size:64
        PLAYER_FIELD_KEYRING_SLOT_LAST = 0x20a + UnitFields.UNIT_END,
        PLAYER_FARSIGHT = 0x20c + UnitFields.UNIT_END, // Size:2
        PLAYER_FIELD_COMBO_TARGET = 0x20e + UnitFields.UNIT_END, // Size:2
        PLAYER_XP = 0x210 + UnitFields.UNIT_END, // Size:1
        PLAYER_NEXT_LEVEL_XP = 0x211 + UnitFields.UNIT_END, // Size:1
        PLAYER_SKILL_INFO_1_1 = 0x212 + UnitFields.UNIT_END, // Size:384
        PLAYER_SKILL_PROP_1_1 = 0x213 + UnitFields.UNIT_END, // Size:384

        PLAYER_CHARACTER_POINTS1 = 0x392 + UnitFields.UNIT_END, // Size:1
        PLAYER_CHARACTER_POINTS2 = 0x393 + UnitFields.UNIT_END, // Size:1
        PLAYER_TRACK_CREATURES = 0x394 + UnitFields.UNIT_END, // Size:1
        PLAYER_TRACK_RESOURCES = 0x395 + UnitFields.UNIT_END, // Size:1
        PLAYER_BLOCK_PERCENTAGE = 0x396 + UnitFields.UNIT_END, // Size:1
        PLAYER_DODGE_PERCENTAGE = 0x397 + UnitFields.UNIT_END, // Size:1
        PLAYER_PARRY_PERCENTAGE = 0x398 + UnitFields.UNIT_END, // Size:1
        PLAYER_CRIT_PERCENTAGE = 0x399 + UnitFields.UNIT_END, // Size:1
        PLAYER_RANGED_CRIT_PERCENTAGE = 0x39a + UnitFields.UNIT_END, // Size:1
        PLAYER_EXPLORED_ZONES_1 = 0x39b + UnitFields.UNIT_END, // Size:64
        PLAYER_REST_STATE_EXPERIENCE = 0x3db + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_COINAGE = 0x3dc + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT0 = 0x3DD + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT1 = 0x3DE + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT2 = 0x3DF + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT3 = 0x3E0 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT4 = 0x3E1 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT0 = 0x3E2 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT1 = 0x3E3 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT2 = 0x3E4 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT3 = 0x3E5 + UnitFields.UNIT_END, // Size:1,
        PLAYER_FIELD_NEGSTAT4 = 0x3E6 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_RESISTANCEBUFFMODSPOSITIVE = 0x3E7 + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_RESISTANCEBUFFMODSNEGATIVE = 0x3EE + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_MOD_DAMAGE_DONE_POS = 0x3F5 + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_MOD_DAMAGE_DONE_NEG = 0x3FC + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_MOD_DAMAGE_DONE_PCT = 0x403 + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_BYTES = 0x40A + UnitFields.UNIT_END, // Size:1
        PLAYER_AMMO_ID = 0x40B + UnitFields.UNIT_END, // Size:1
        PLAYER_SELF_RES_SPELL = 0x40C + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_PVP_MEDALS = 0x40D + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_BUYBACK_PRICE_1 = 0x40E + UnitFields.UNIT_END, // count=12
        PLAYER_FIELD_BUYBACK_PRICE_LAST = 0x419 + UnitFields.UNIT_END,
        PLAYER_FIELD_BUYBACK_TIMESTAMP_1 = 0x41A + UnitFields.UNIT_END, // count=12
        PLAYER_FIELD_BUYBACK_TIMESTAMP_LAST = 0x425 + UnitFields.UNIT_END,
        PLAYER_FIELD_SESSION_KILLS = 0x426 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_YESTERDAY_KILLS = 0x427 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LAST_WEEK_KILLS = 0x428 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_THIS_WEEK_KILLS = 0x429 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_THIS_WEEK_CONTRIBUTION = 0x42a + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LIFETIME_HONORABLE_KILLS = 0x42b + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LIFETIME_DISHONORABLE_KILLS = 0x42c + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_YESTERDAY_CONTRIBUTION = 0x42d + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LAST_WEEK_CONTRIBUTION = 0x42e + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LAST_WEEK_RANK = 0x42f + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_BYTES2 = 0x430 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_WATCHED_FACTION_INDEX = 0x431 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_COMBAT_RATING_1 = 0x432 + UnitFields.UNIT_END, // Size:20

        PLAYER_END = 0x446 + UnitFields.UNIT_END
    }
    internal enum ManaTypes
    {
        TypeMana = 0,
        TypeRage = 1,
        TypeFocus = 2,
        TypeEnergy = 3,
        TypeHappiness = 4,
        TypeHealth = -2
    }
    internal class CharacterHelper
    {
        public static ManaTypes GetClassManaType(Classes classe)
        {
            switch (classe)
            {
                case Classes.Rogue:
                    return ManaTypes.TypeEnergy;
                case Classes.Warrior:
                    return ManaTypes.TypeRage;
                default:
                    return ManaTypes.TypeMana;
            }
        }
        internal static int GetRaceModel(Race race, Gender gender)
        {
            switch (race)
            {
                case Race.Human:
                    return 49 + (int) gender;
                case Race.Orc:
                    return 51 + (int) gender;
                case Race.Dwarf:
                    return 53 + (int) gender;
                case Race.NightElf:
                    return 55 + (int) gender;
                case Race.Undead:
                    return 57 + (int) gender;
                case Race.Tauren:
                    return 59 + (int) gender;
                case Race.Gnome:
                    return 1563 + (int) gender;
                case Race.Troll:
                    return 1478 + (int) gender;
            }

            return 16358 + (int) Gender.Male;
        }

        internal static float GetScale(Race race, Gender gender)
        {
            switch (race)
            {
                case Race.Tauren when gender == Gender.Male:
                    return 1.3f;
                case Race.Tauren:
                    return 1.25f;
                default:
                    return 1f;
            }
        }
    }
    internal class PlayerEntity : UnitEntity
    {
        public new int Model;

        public PlayerEntity(Character character)
            : base(new ObjectGuid((uint) character.ID, TypeId.TypeidPlayer, HighGuid.HighguidPlayer))
        {
            Character = character;
            // KnownPlayers = new List<PlayerEntity>();
            // KnownCreatures = new List<SpawnCreatures>();

            // TutorialFlags = new TutorialFlags(character.TutorialFlags);

            // var chrRaces = MainProgram.ChrRacesReader.GetData(character.Race);

            Model = (int) CharacterHelper.GetRaceModel(character.Race, character.Gender);
            Scale = CharacterHelper.GetScale(character.Race, character.Gender);

            SetUpdateField((int) ObjectFields.Type, (uint) 0x19); // 25
            SetUpdateField((int) ObjectFields.ScaleX, Size);
            SetUpdateField((int) ObjectFields.Padding, 0);

            SetUpdateField((int) UnitFields.UNIT_FIELD_TARGET, (ulong) 0);

            SetUpdateField((int) UnitFields.UNIT_FIELD_HEALTH, character.Stats.Life);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER1, character.Stats.Mana);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER2, character.Stats.Rage);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER3, 0);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER4, character.Stats.Energy);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER5, 0);

            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXHEALTH, character.Stats.Life);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER1, character.Stats.Mana);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER2, character.Stats.Rage);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER3, 0); // Focus
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER4, character.Stats.Energy);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER5, 0);


            SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, character.Level);
            SetUpdateField((int) UnitFields.UNIT_FIELD_FACTIONTEMPLATE, (int) 1); // int chrRaces.FactionId);

            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0, BitConverter.ToUInt32(new[]
            {
                (byte) character.Race,
                (byte) character.Class,
                (byte) character.Gender,
                (byte) CharacterHelper.GetClassManaType(character.Class)
            }, 0));

            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT0, 13); //character.Stats.Strength);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT1, 14); //character.Stats.Agility);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT2, 15); //character.Stats.Stamina);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT3, 16); //character.Stats.Intellect);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT4, 17); //character.Stats.Spirit);

            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES, 18); //character.SubResistances.Armor
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_01, 19); //character.SubResistances.
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_02, 20); //character.SubResistances.Fire
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_03, 21); //character.SubResistances.Nature
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_04, 22); //character.SubResistances.Frost
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_05, 23); //character.SubResistances.Shadow
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_06, 24); //character.SubResistances.Arcane

            SetUpdateField((int) UnitFields.UNIT_FIELD_FLAGS, character.Flag);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BASE_MANA, character.Stats.Mana);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BASE_HEALTH, character.Stats.Life);

            SetUpdateField((int) UnitFields.UNIT_FIELD_DISPLAYID, Model);
            SetUpdateField((int) UnitFields.UNIT_FIELD_NATIVEDISPLAYID, Model);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MOUNTDISPLAYID, 0);

            // FLAG <GM>
            SetUpdateField((int) PlayerFields.PLAYER_FLAGS, 0x00000008);

            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_1, (byte) 1); // byte character.StandState);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_2, 0);

            SetUpdateField((int) PlayerFields.PLAYER_BYTES, BitConverter.ToUInt32(new[]
            {
                character.Skin,
                character.Face,
                character.HairStyle,
                character.HairColor
            }, 0));

            SetUpdateField((int) PlayerFields.PLAYER_BYTES_2, BitConverter.ToUInt32(new byte[]
            {
                character.FacialHair,
                0,
                0,
                3 //RestedState
            }));

            SetUpdateField((int) PlayerFields.PLAYER_BYTES_3, (uint) character.Gender);
            SetUpdateField((int) PlayerFields.PLAYER_XP, 0);
            SetUpdateField((int) PlayerFields.PLAYER_NEXT_LEVEL_XP, 400);
            SetUpdateField((int) PlayerFields.PLAYER_SKILL_INFO_1_1, 26);
            SetUpdateField((int) PlayerFields.PLAYER_FIELD_WATCHED_FACTION_INDEX, (int) 1); // int character.WatchFaction);

            SkillGenerate();
        }

        public override int DataLength => (int) PlayerFields.PLAYER_END - 0x4;

        public Character Character { get; }
        public override string Name => Character.Name;
        public WorldClient Session { get; set; }
        // public List<PlayerEntity> KnownPlayers { get; set; }
        // public List<SpawnCreatures> KnownCreatures { get; set; }
        // public TutorialFlags TutorialFlags { get; set; }

        private void SkillGenerate()
        {
            // var a = 0;
            // foreach (var skill in Character.SubSkills)
            // {
            //     SetUpdateField((int) PlayerFields.PLAYER_SKILL_INFO_1_1 + a * 3, skill.Skill);
            //     SetUpdateField((int) PlayerFields.PLAYER_SKILL_INFO_1_1 + a * 3 + 1, skill.Value + (skill.Max << 16));
            //     a++;
            // }
        }
    }
    internal class UnitEntity : ObjectEntity
    {
        public UnitEntity(ObjectGuid objectGuid) : base(objectGuid)
        {
        }

        public TypeId TypeId => TypeId.TypeidUnit;
        public override int DataLength => (int) UnitFields.UNIT_END - 0x4;
    }
    internal enum UnitFields
    {
        UNIT_FIELD_CHARM = 0x00 + ObjectFields.End, // Size:2
        UNIT_FIELD_SUMMON = 0x02 + ObjectFields.End, // Size:2
        UNIT_FIELD_CHARMEDBY = 0x04 + ObjectFields.End, // Size:2
        UNIT_FIELD_SUMMONEDBY = 0x06 + ObjectFields.End, // Size:2
        UNIT_FIELD_CREATEDBY = 0x08 + ObjectFields.End, // Size:2
        UNIT_FIELD_TARGET = 0x0A + ObjectFields.End, // Size:2
        UNIT_FIELD_PERSUADED = 0x0C + ObjectFields.End, // Size:2
        UNIT_FIELD_CHANNEL_OBJECT = 0x0E + ObjectFields.End, // Size:2
        UNIT_FIELD_HEALTH = 0x10 + ObjectFields.End, // Size:1
        UNIT_FIELD_POWER1 = 0x11 + ObjectFields.End, // Size:1
        UNIT_FIELD_POWER2 = 0x12 + ObjectFields.End, // Size:1
        UNIT_FIELD_POWER3 = 0x13 + ObjectFields.End, // Size:1
        UNIT_FIELD_POWER4 = 0x14 + ObjectFields.End, // Size:1
        UNIT_FIELD_POWER5 = 0x15 + ObjectFields.End, // Size:1
        UNIT_FIELD_MAXHEALTH = 0x16 + ObjectFields.End, // Size:1 
        UNIT_FIELD_MAXPOWER1 = 0x17 + ObjectFields.End, // Size:1
        UNIT_FIELD_MAXPOWER2 = 0x18 + ObjectFields.End, // Size:1
        UNIT_FIELD_MAXPOWER3 = 0x19 + ObjectFields.End, // Size:1
        UNIT_FIELD_MAXPOWER4 = 0x1A + ObjectFields.End, // Size:1
        UNIT_FIELD_MAXPOWER5 = 0x1B + ObjectFields.End, // Size:1
        UNIT_FIELD_LEVEL = 0x1C + ObjectFields.End, // Size:1
        UNIT_FIELD_FACTIONTEMPLATE = 0x1D + ObjectFields.End, // Size:1
        UNIT_FIELD_BYTES_0 = 0x1E + ObjectFields.End, // Size:1
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY = 0x1F + ObjectFields.End, // Size:3
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY_01 = 0x20 + ObjectFields.End,
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY_02 = 0x21 + ObjectFields.End,
        UNIT_VIRTUAL_ITEM_INFO = 0x22 + ObjectFields.End, // Size:6
        UNIT_VIRTUAL_ITEM_INFO_01 = 0x23 + ObjectFields.End,
        UNIT_VIRTUAL_ITEM_INFO_02 = 0x24 + ObjectFields.End,
        UNIT_VIRTUAL_ITEM_INFO_03 = 0x25 + ObjectFields.End,
        UNIT_VIRTUAL_ITEM_INFO_04 = 0x26 + ObjectFields.End,
        UNIT_VIRTUAL_ITEM_INFO_05 = 0x27 + ObjectFields.End,
        UNIT_FIELD_FLAGS = 0x28 + ObjectFields.End, // Size:1
        UNIT_FIELD_AURA = 0x29 + ObjectFields.End, // Size:48
        UNIT_FIELD_AURA_LAST = 0x58 + ObjectFields.End,
        UNIT_FIELD_AURAFLAGS = 0x59 + ObjectFields.End, // Size:6
        UNIT_FIELD_AURAFLAGS_01 = 0x5a + ObjectFields.End,
        UNIT_FIELD_AURAFLAGS_02 = 0x5b + ObjectFields.End,
        UNIT_FIELD_AURAFLAGS_03 = 0x5c + ObjectFields.End,
        UNIT_FIELD_AURAFLAGS_04 = 0x5d + ObjectFields.End,
        UNIT_FIELD_AURAFLAGS_05 = 0x5e + ObjectFields.End,
        UNIT_FIELD_AURALEVELS = 0x5f + ObjectFields.End, // Size:12
        UNIT_FIELD_AURALEVELS_LAST = 0x6a + ObjectFields.End,
        UNIT_FIELD_AURAAPPLICATIONS = 0x6b + ObjectFields.End, // Size:12
        UNIT_FIELD_AURAAPPLICATIONS_LAST = 0x76 + ObjectFields.End,
        UNIT_FIELD_AURASTATE = 0x77 + ObjectFields.End, // Size:1
        UNIT_FIELD_BASEATTACKTIME = 0x78 + ObjectFields.End, // Size:2
        UNIT_FIELD_OFFHANDATTACKTIME = 0x79 + ObjectFields.End, // Size:2
        UNIT_FIELD_RANGEDATTACKTIME = 0x7a + ObjectFields.End, // Size:1
        UNIT_FIELD_BOUNDINGRADIUS = 0x7b + ObjectFields.End, // Size:1
        UNIT_FIELD_COMBATREACH = 0x7c + ObjectFields.End, // Size:1
        UNIT_FIELD_DISPLAYID = 0x7d + ObjectFields.End, // Size:1
        UNIT_FIELD_NATIVEDISPLAYID = 0x7e + ObjectFields.End, // Size:1
        UNIT_FIELD_MOUNTDISPLAYID = 0x7f + ObjectFields.End, // Size:1
        UNIT_FIELD_MINDAMAGE = 0x80 + ObjectFields.End, // Size:1
        UNIT_FIELD_MAXDAMAGE = 0x81 + ObjectFields.End, // Size:1
        UNIT_FIELD_MINOFFHANDDAMAGE = 0x82 + ObjectFields.End, // Size:1
        UNIT_FIELD_MAXOFFHANDDAMAGE = 0x83 + ObjectFields.End, // Size:1
        UNIT_FIELD_BYTES_1 = 0x84 + ObjectFields.End, // Size:1
        UNIT_FIELD_PETNUMBER = 0x85 + ObjectFields.End, // Size:1
        UNIT_FIELD_PET_NAME_TIMESTAMP = 0x86 + ObjectFields.End, // Size:1
        UNIT_FIELD_PETEXPERIENCE = 0x87 + ObjectFields.End, // Size:1
        UNIT_FIELD_PETNEXTLEVELEXP = 0x88 + ObjectFields.End, // Size:1
        UNIT_DYNAMIC_FLAGS = 0x89 + ObjectFields.End, // Size:1
        UNIT_CHANNEL_SPELL = 0x8a + ObjectFields.End, // Size:1
        UNIT_MOD_CAST_SPEED = 0x8b + ObjectFields.End, // Size:1
        UNIT_CREATED_BY_SPELL = 0x8c + ObjectFields.End, // Size:1
        UNIT_NPC_FLAGS = 0x8d + ObjectFields.End, // Size:1
        UNIT_NPC_EMOTESTATE = 0x8e + ObjectFields.End, // Size:1
        UNIT_TRAINING_POINTS = 0x8f + ObjectFields.End, // Size:1
        UNIT_FIELD_STAT0 = 0x90 + ObjectFields.End, // Size:1
        UNIT_FIELD_STAT1 = 0x91 + ObjectFields.End, // Size:1
        UNIT_FIELD_STAT2 = 0x92 + ObjectFields.End, // Size:1
        UNIT_FIELD_STAT3 = 0x93 + ObjectFields.End, // Size:1
        UNIT_FIELD_STAT4 = 0x94 + ObjectFields.End, // Size:1
        UNIT_FIELD_RESISTANCES = 0x95 + ObjectFields.End, // Size:7
        UNIT_FIELD_RESISTANCES_01 = 0x96 + ObjectFields.End,
        UNIT_FIELD_RESISTANCES_02 = 0x97 + ObjectFields.End,
        UNIT_FIELD_RESISTANCES_03 = 0x98 + ObjectFields.End,
        UNIT_FIELD_RESISTANCES_04 = 0x99 + ObjectFields.End,
        UNIT_FIELD_RESISTANCES_05 = 0x9a + ObjectFields.End,
        UNIT_FIELD_RESISTANCES_06 = 0x9b + ObjectFields.End,
        UNIT_FIELD_BASE_MANA = 0x9c + ObjectFields.End, // Size:1
        UNIT_FIELD_BASE_HEALTH = 0x9d + ObjectFields.End, // Size:1
        UNIT_FIELD_BYTES_2 = 0x9e + ObjectFields.End, // Size:1
        UNIT_FIELD_ATTACK_POWER = 0x9f + ObjectFields.End, // Size:1
        UNIT_FIELD_ATTACK_POWER_MODS = 0xa0 + ObjectFields.End, // Size:1
        UNIT_FIELD_ATTACK_POWER_MULTIPLIER = 0xa1 + ObjectFields.End, // Size:1
        UNIT_FIELD_RANGED_ATTACK_POWER = 0xa2 + ObjectFields.End, // Size:1
        UNIT_FIELD_RANGED_ATTACK_POWER_MODS = 0xa3 + ObjectFields.End, // Size:1
        UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER = 0xa4 + ObjectFields.End, // Size:1
        UNIT_FIELD_MINRANGEDDAMAGE = 0xa5 + ObjectFields.End, // Size:1
        UNIT_FIELD_MAXRANGEDDAMAGE = 0xa6 + ObjectFields.End, // Size:1
        UNIT_FIELD_POWER_COST_MODIFIER = 0xa7 + ObjectFields.End, // Size:7
        UNIT_FIELD_POWER_COST_MODIFIER_01 = 0xa8 + ObjectFields.End,
        UNIT_FIELD_POWER_COST_MODIFIER_02 = 0xa9 + ObjectFields.End,
        UNIT_FIELD_POWER_COST_MODIFIER_03 = 0xaa + ObjectFields.End,
        UNIT_FIELD_POWER_COST_MODIFIER_04 = 0xab + ObjectFields.End,
        UNIT_FIELD_POWER_COST_MODIFIER_05 = 0xac + ObjectFields.End,
        UNIT_FIELD_POWER_COST_MODIFIER_06 = 0xad + ObjectFields.End,
        UNIT_FIELD_POWER_COST_MULTIPLIER = 0xae + ObjectFields.End, // Size:7
        UNIT_FIELD_POWER_COST_MULTIPLIER_01 = 0xaf + ObjectFields.End,
        UNIT_FIELD_POWER_COST_MULTIPLIER_02 = 0xb0 + ObjectFields.End,
        UNIT_FIELD_POWER_COST_MULTIPLIER_03 = 0xb1 + ObjectFields.End,
        UNIT_FIELD_POWER_COST_MULTIPLIER_04 = 0xb2 + ObjectFields.End,
        UNIT_FIELD_POWER_COST_MULTIPLIER_05 = 0xb3 + ObjectFields.End,
        UNIT_FIELD_POWER_COST_MULTIPLIER_06 = 0xb4 + ObjectFields.End,
        UNIT_FIELD_PADDING = 0xb5 + ObjectFields.End,
        UNIT_END = 0xb6 + ObjectFields.End
    }
    internal enum ObjectFields
    {
        Guid = 0x0, // 0x000 - Size: 2 - Type: GUID  - Flags: PUBLIC
        Type = 0x2, // 0x002 - Size: 1 - Type: INT   - Flags: PUBLIC
        Entry = 0x3, // 0x003 - Size: 1 - Type: INT   - Flags: PUBLIC
        ScaleX = 0x4, // 0x004 - Size: 1 - Type: FLOAT - Flags: PUBLIC
        Padding = 0x5, // 0x005 - Size: 1 - Type: INT   - Flags: NONE
        End = 0x6
    }
    internal enum HighGuid
    {
        HighguidPlayer = 0x0000,
        HighguidItem = 0x4700,
        HighguidContainer = 0x4700,
        HighguidDynamicobject = 0xF100,
        HighguidGameobject = 0xF110,
        HighguidTransport = 0xF120,
        HighguidUnit = 0xF130,
        HighguidPet = 0xF140,
        HighguidVehicle = 0xF150,
        HighguidCorpse = 0xF500,
        HighguidMoTransport = 0x1FC0
    }
    internal enum TypeId : byte
    {
        TypeidObject = 0,
        TypeidItem = 1,
        TypeidContainer = 2,
        TypeidUnit = 3,
        TypeidPlayer = 4,
        TypeidGameobject = 5,
        TypeidDynamicobject = 6,
        TypeidCorpse = 7
    }
    internal class ObjectGuid
    {
        public ObjectGuid(ulong guid)
        {
            RawGuid = guid;
        }

        public ObjectGuid(uint index, TypeId type, HighGuid high)
        {
            TypeId = type;
            HighGuid = high;
            RawGuid = index | ((ulong) type << 24) | ((ulong) high << 48);
        }

        public TypeId TypeId { get; }
        public HighGuid HighGuid { get; }
        public ulong RawGuid { get; }
    }
    internal class ObjectEntity : BaseEntity
    {
        public ObjectEntity(ObjectGuid objectGuid)
        {
            ObjectGuid = objectGuid;
            Guid = ObjectGuid.RawGuid;
        }

        public ObjectGuid ObjectGuid { get; set; }

        public override int DataLength => (int) ObjectFields.End;

        public ulong Guid
        {
            get => (ulong) UpdateData[ObjectFields.Guid];
            set => SetUpdateField((int) ObjectFields.Guid, value);
        }

        public byte Type
        {
            get => (byte) UpdateData[(int) ObjectFields.Type];
            set => SetUpdateField((int) ObjectFields.Type, value);
        }

        public int Entry
        {
            get => (int) UpdateData[(int) ObjectFields.Entry];
            set => SetUpdateField((int) ObjectFields.Entry, value);
        }

        public float Scale
        {
            get => (float) UpdateData[(int) ObjectFields.ScaleX];
            set => SetUpdateField((int) ObjectFields.ScaleX, value);
        }
    }
    internal class BaseEntity
    {
        public static int Level = 1;
        public int Model = 0;

        // Base Stats
        public float Size = 1.0f;

        public BaseEntity()
        {
            MaskSize = (DataLength + 32) / 32;
            Mask = new BitArray(DataLength, false);
            UpdateData = new Hashtable();
        }

        public int MaskSize { get; }
        public BitArray Mask { get; }
        public Hashtable UpdateData { get; }
        public int UpdateCount { get; private set; }

        public virtual int DataLength { get; internal set; }
        public virtual string Name { get; set; }

        public void SetUpdateField<T>(int index, T value, byte offset = 0)
        {
            UpdateCount++;
            switch (value.GetType().Name)
            {
                case "SByte":
                case "Int16":
                {
                    Mask.Set(index, true);

                    if (UpdateData.ContainsKey(index))
                        UpdateData[index] = (int) UpdateData[index] |
                                            ((int) Convert.ChangeType(value, typeof(int)) <<
                                             (offset * (value.GetType().Name == "Byte" ? 8 : 16)));
                    else
                        UpdateData[index] = (int) Convert.ChangeType(value, typeof(int)) <<
                                            (offset * (value.GetType().Name == "Byte" ? 8 : 16));

                    break;
                }
                case "Byte":
                case "UInt16":
                {
                    Mask.Set(index, true);

                    if (UpdateData.ContainsKey(index))
                        UpdateData[index] = (uint) UpdateData[index] |
                                            ((uint) Convert.ChangeType(value, typeof(uint)) <<
                                             (offset * (value.GetType().Name == "Byte" ? 8 : 16)));
                    else
                        UpdateData[index] = (uint) Convert.ChangeType(value, typeof(uint)) <<
                                            (offset * (value.GetType().Name == "Byte" ? 8 : 16));

                    break;
                }
                case "Int64":
                {
                    Mask.Set(index, true);
                    Mask.Set(index + 1, true);

                    var tmpValue = (long) Convert.ChangeType(value, typeof(long));

                    UpdateData[index] = (uint) (tmpValue & int.MaxValue);
                    UpdateData[index + 1] = (uint) ((tmpValue >> 32) & int.MaxValue);

                    break;
                }
                case "UInt64":
                {
                    Mask.Set(index, true);
                    Mask.Set(index + 1, true);

                    var tmpValue = (ulong) Convert.ChangeType(value, typeof(ulong));

                    UpdateData[index] = (uint) (tmpValue & uint.MaxValue);
                    UpdateData[index + 1] = (uint) ((tmpValue >> 32) & uint.MaxValue);

                    break;
                }
                default:
                {
                    Mask.Set(index, true);
                    UpdateData[index] = value;

                    break;
                }
            }
        }

        public void WriteUpdateFields(PacketWriter writer)
        {
            writer.WriteUInt8((byte) MaskSize);
            
            // writer.WriteBitArray(Mask, MaskSize * 4); // Int32 = 4 Bytes
            // Start WriteBitArray
            var bufferarray = new byte[Convert.ToByte((Mask.Length + 8) / 8) + 1];
            Mask.CopyTo(bufferarray, 0);
            writer.WriteByteArrayWithLength(bufferarray, MaskSize * 4);
            // End WriteBitArray

            for (var i = 0; i < Mask.Count; i++)
            {
                if (!Mask.Get(i)) continue;
                try
                {
                    switch (UpdateData[i].GetType().Name)
                    {
                        case "UInt32":
                            writer.WriteUInt32((uint) UpdateData[i]);
                            break;
                        case "Single":
                            writer.WriteFloat((float) UpdateData[i]);
                            break;
                        default:
                            writer.WriteInt32((int) UpdateData[i]);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Logger.Log($"Error in WriteUpdateFields, {e.Message}");
                }
            }

            Mask.SetAll(false);
            UpdateCount = 0;
        }
    }
}