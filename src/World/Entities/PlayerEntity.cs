using System;
using Classic.Shared.Data;
using Classic.World.Data;
using Classic.World.Entities.Enums;
using Classic.World.Entities.Utils;
using Classic.World.Extensions;

namespace Classic.World.Entities;

public class PlayerEntity : UnitEntity
{
    public new int Model;

    public PlayerEntity(Character character, int build)
        : base(new ObjectGuid((uint)character.Id, TypeId.TypeidPlayer, HighGuid.HighguidPlayer), build)
    {
        Model = character.Race.GetModel(character.Gender);
        Scale = character.Race.GetScale(character.Gender);

        switch (build)
        {
            case ClientBuild.Vanilla:
                this.SetUpdateField_Vanilla(character);
                break;
            case ClientBuild.TBC:
                this.SetUpdateField_TBC(character);
                break;
        }
    }

    protected override int GetDatalength(int build) => build switch
    {
        ClientBuild.Vanilla => (int)PlayerFields_Vanilla.PLAYER_END - 0x4,
        ClientBuild.TBC => (int)PlayerFields_TBC.PLAYER_END - 0x4,
        _ => throw new NotImplementedException($"PlayerEntity.GetDatalength(build: {build})"),
    };

    public ulong CharacterId { get; set; }
    public ulong TargetId { get; set; }

    private void SetUpdateField_Vanilla(Character character)
    {
        SetUpdateField((int)ObjectFields.Type, (uint)0x19);
        SetUpdateField((int)ObjectFields.ScaleX, Size);
        SetUpdateField((int)ObjectFields.Padding, 0);

        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_TARGET, (ulong)0);

        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_HEALTH, character.Stats.Life);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_POWER1, character.Stats.Mana);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_POWER2, character.Stats.Rage);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_POWER3, character.Stats.Focus);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_POWER4, character.Stats.Energy);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_POWER5, 0);

        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MAXHEALTH, character.MaxStats.Life);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MAXPOWER1, character.MaxStats.Mana);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MAXPOWER2, character.MaxStats.Rage);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MAXPOWER3, character.MaxStats.Focus);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MAXPOWER4, character.MaxStats.Energy);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MAXPOWER5, 0);


        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_LEVEL, character.Level);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_FACTIONTEMPLATE, (int)character.Race);

        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BYTES_0, BitConverter.ToUInt32(new[]
        {
            (byte) character.Race,
            (byte) character.Class,
            (byte) character.Gender,
            (byte) character.Class.GetManaType()
        }));

        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_STAT0, character.Attributes.Strength);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_STAT1, character.Attributes.Agility);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_STAT2, character.Attributes.Stamina);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_STAT3, character.Attributes.Intellect);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_STAT4, character.Attributes.Spirit);

        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_RESISTANCES, character.Resistances.Armor);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_RESISTANCES_01, 0); // unknown
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_RESISTANCES_02, character.Resistances.Fire);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_RESISTANCES_03, character.Resistances.Nature);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_RESISTANCES_04, character.Resistances.Frost);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_RESISTANCES_05, character.Resistances.Shadow);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_RESISTANCES_06, character.Resistances.Arcane);

        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_FLAGS, character.Flag);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BASE_MANA, character.Stats.Mana);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BASE_HEALTH, character.Stats.Life);

        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_DISPLAYID, Model);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_NATIVEDISPLAYID, Model);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MOUNTDISPLAYID, 0);

        // FLAG <GM>
        SetUpdateField((int)PlayerFields_Vanilla.PLAYER_FLAGS, 0x00000008);

        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BYTES_1, (byte)character.StandState);
        SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BYTES_2, 0);

        SetUpdateField((int)PlayerFields_Vanilla.PLAYER_BYTES, BitConverter.ToUInt32(new[]
        {
            character.Skin,
            character.Face,
            character.HairStyle,
            character.HairColor
        }));

        SetUpdateField((int)PlayerFields_Vanilla.PLAYER_BYTES_2, BitConverter.ToUInt32(new byte[]
        {
            character.FacialHair,
            0,
            0,
            (byte) character.RestedState
        }));

        SetUpdateField((int)PlayerFields_Vanilla.PLAYER_BYTES_3, (uint)character.Gender);
        SetUpdateField((int)PlayerFields_Vanilla.PLAYER_XP, 0);
        SetUpdateField((int)PlayerFields_Vanilla.PLAYER_NEXT_LEVEL_XP, 400);
        SetUpdateField((int)PlayerFields_Vanilla.PLAYER_SKILL_INFO_1_1, 26); // Needed??
        SetUpdateField((int)PlayerFields_Vanilla.PLAYER_FIELD_WATCHED_FACTION_INDEX, character.WatchFaction);

        SkillGenerate_Vanilla(character);
    }

    private void SetUpdateField_TBC(Character character)
    {
        // ObjectField.GUID,
        SetUpdateField((int)ObjectFields.Guid, character.Id.ToPackedUInt64());

        // ObjectField.TYPE,
        SetUpdateField((int)ObjectFields.Type, (uint)0x19);

        // ObjectField.SCALE_X,
        SetUpdateField((int)ObjectFields.ScaleX, Size);

        // UnitField.HEALTH,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_HEALTH, character.Stats.Life);

        // UnitField.POWER1-5,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_POWER1, character.Stats.Mana);
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_POWER2, character.Stats.Rage);
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_POWER3, character.Stats.Focus);
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_POWER4, character.Stats.Energy);
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_POWER5, 0);

        // UnitField.MAXHEALTH,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MAXHEALTH, character.MaxStats.Life);

        // UnitField.MAXPOWER1-5,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MAXPOWER1, character.MaxStats.Mana);
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MAXPOWER2, character.MaxStats.Rage);
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MAXPOWER3, character.MaxStats.Focus);
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MAXPOWER4, character.MaxStats.Energy);
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MAXPOWER5, 0);

        // UnitField.LEVEL,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_LEVEL, character.Level);

        // UnitField.FACTIONTEMPLATE,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_FACTIONTEMPLATE, (int)character.Race);

        // UnitField.BYTES_0,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_BYTES_0, BitConverter.ToUInt32(new[]
        {
            (byte) character.Race,
            (byte) character.Class,
            (byte) character.Gender,
            (byte) character.Class.GetManaType()
        }));

        // UnitField.FLAGS,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_FLAGS, (int)character.Flag);

        // UnitField.BASEATTACKTIME,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_BASEATTACKTIME, 1);

        // UnitField.BOUNDINGRADIUS,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_BOUNDINGRADIUS, 1f);

        // UnitField.COMBATREACH,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_COMBATREACH, 1f);

        // UnitField.DISPLAYID,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_DISPLAYID, Model);

        // UnitField.NATIVEDISPLAYID,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_NATIVEDISPLAYID, Model);

        // UnitField.MOUNTDISPLAYID,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MOUNTDISPLAYID, 0);

        // UnitField.MINDAMAGE,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MINDAMAGE, 1f);

        // UnitField.MAXDAMAGE,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MAXDAMAGE, 2f);

        // UnitField.MINOFFHANDDAMAGE,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MINOFFHANDDAMAGE, 1f);

        // UnitField.MAXOFFHANDDAMAGE,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MAXOFFHANDDAMAGE, 1f);

        // UnitField.BYTES_1,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_BYTES_1, (byte)character.StandState);

        // UnitField.MOD_CAST_SPEED,
        SetUpdateField((int)UnitFields_TBC.UNIT_MOD_CAST_SPEED, 1f);

        // UnitField.STAT0-4,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_STAT0, character.Attributes.Strength);
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_STAT1, character.Attributes.Agility);
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_STAT2, character.Attributes.Stamina);
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_STAT3, character.Attributes.Intellect);
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_STAT4, character.Attributes.Spirit);

        // UnitField.RESISTANCES,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_RESISTANCES, BitConverter.ToUInt32(new[]
        {
            (byte)character.Resistances.Armor,
            (byte)0,
            (byte)character.Resistances.Fire,
            (byte)character.Resistances.Nature,
            (byte)character.Resistances.Shadow,
            (byte)character.Resistances.Arcane,
            //(byte)0,
        }));

        // UnitField.BASE_MANA,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_BASE_MANA, character.Stats.Mana);

        // UnitField.BYTES_2,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_BYTES_2, (byte)0);

        // UnitField.ATTACK_POWER,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_ATTACK_POWER, 1);

        // UnitField.ATTACK_POWER_MODS,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_ATTACK_POWER_MODS, 1);

        // UnitField.RANGED_ATTACK_POWER,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_RANGED_ATTACK_POWER, 1);

        // UnitField.RANGED_ATTACK_POWER_MODS,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_RANGED_ATTACK_POWER_MODS, 1);

        // UnitField.MINRANGEDDAMAGE,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MINRANGEDDAMAGE, 1f);

        // UnitField.MAXRANGEDDAMAGE,
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MAXRANGEDDAMAGE, 1f);

        // PlayerField.FLAGS,
        SetUpdateField((int)PlayerFields_TBC.PLAYER_FLAGS, 0x00000008); // FLAG <GM>

        // PlayerField.BYTES_1,
        SetUpdateField((int)PlayerFields_TBC.PLAYER_BYTES, BitConverter.ToUInt32(new[]
        {
            character.Skin,
            character.Face,
            character.HairStyle,
            character.HairColor
        }));

        // PlayerField.BYTES_2,
        SetUpdateField((int)PlayerFields_TBC.PLAYER_BYTES_2, BitConverter.ToUInt32(new byte[]
        {
            character.FacialHair,
            0,
            0, // Bankbag Slots
            (byte) character.RestedState
        }));

        // PlayerField.BYTES_3,
        SetUpdateField((int)PlayerFields_TBC.PLAYER_BYTES_3, BitConverter.ToUInt32(new byte[]
        {
            (byte)character.Gender,
            0, // Drunkness
            0,
            0, // PvP Rank?
        }));

        // PlayerField.XP,
        SetUpdateField((int)PlayerFields_TBC.PLAYER_XP, 0);

        // PlayerField.NEXT_LEVEL_XP,
        SetUpdateField((int)PlayerFields_TBC.PLAYER_NEXT_LEVEL_XP, 400);

        // PlayerField.CHARACTER_POINTS1,
        SetUpdateField((int)PlayerFields_TBC.PLAYER_CHARACTER_POINTS1, 1);

        // PlayerField.CHARACTER_POINTS2,
        SetUpdateField((int)PlayerFields_TBC.PLAYER_CHARACTER_POINTS2, 1);

        // PlayerField.BLOCK_PERCENTAGE,
        SetUpdateField((int)PlayerFields_TBC.PLAYER_BLOCK_PERCENTAGE, 1f);

        // PlayerField.DODGE_PERCENTAGE,
        SetUpdateField((int)PlayerFields_TBC.PLAYER_DODGE_PERCENTAGE, 1f);

        // PlayerField.PARRY_PERCENTAGE,
        SetUpdateField((int)PlayerFields_TBC.PLAYER_PARRY_PERCENTAGE, 1f);

        // PlayerField.CRIT_PERCENTAGE,
        SetUpdateField((int)PlayerFields_TBC.PLAYER_CRIT_PERCENTAGE, 1f);

        // PlayerField.REST_STATE_EXPERIENCE,
        SetUpdateField((int)PlayerFields_TBC.PLAYER_REST_STATE_EXPERIENCE, 1);

        // PlayerField.COINAGE
        SetUpdateField((int)PlayerFields_TBC.PLAYER_FIELD_COINAGE, 1);

        SetUpdateField((int)ObjectFields.Padding, 0);
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_TARGET, (ulong)0);
        SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_BASE_HEALTH, character.Stats.Life);
        SetUpdateField((int)PlayerFields_TBC.PLAYER_FIELD_WATCHED_FACTION_INDEX, character.WatchFaction);

        SkillGenerate_TBC(character);
    }

    private void SkillGenerate_Vanilla(Character character)
    {
        var a = 0;
        foreach (var skill in character.Skills)
        {
            SetUpdateField((int)PlayerFields_Vanilla.PLAYER_SKILL_INFO_1_1 + a * 3, skill.Id);
            SetUpdateField((int)PlayerFields_Vanilla.PLAYER_SKILL_INFO_1_1 + a * 3 + 1, skill.Current + (skill.Max << 16));
            a++;
        }
    }

    private void SkillGenerate_TBC(Character character)
    {
        var a = 0;
        foreach (var skill in character.Skills)
        {
            SetUpdateField((int)PlayerFields_TBC.PLAYER_SKILL_INFO_1_1 + a * 3, skill.Id);
            SetUpdateField((int)PlayerFields_TBC.PLAYER_SKILL_INFO_1_1 + a * 3 + 1, skill.Current + (skill.Max << 16));
            a++;
        }
    }
}
