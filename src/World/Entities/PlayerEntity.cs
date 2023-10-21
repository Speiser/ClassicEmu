using System;
using Classic.Shared.Data;
using Classic.World.Data;
using Classic.World.Entities.Enums;
using Classic.World.Extensions;

namespace Classic.World.Entities;

public class PlayerEntity : ObjectEntity
{
    public PlayerEntity(Character character, int build) : base(character.Id, build)
    {
        this.SetUpdateField((int)ObjectFields.Type, (uint)0x19);
        this.SetUpdateField((int)ObjectFields.ScaleX, character.Race.GetScale(character.Gender));
        this.SetUpdateField((int)ObjectFields.Padding, 0);

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
    public Creature Target { get; set; }
    public SheathState SheathState { get; set; }

    private void SetUpdateField_Vanilla(Character character)
    {
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_TARGET, (ulong)0); // TODO Use TargetId?

        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_HEALTH, character.Stats.Life);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_POWER1, character.Stats.Mana);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_POWER2, character.Stats.Rage);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_POWER3, character.Stats.Focus);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_POWER4, character.Stats.Energy);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_POWER5, 0);

        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MAXHEALTH, character.MaxStats.Life);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MAXPOWER1, character.MaxStats.Mana);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MAXPOWER2, character.MaxStats.Rage);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MAXPOWER3, character.MaxStats.Focus);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MAXPOWER4, character.MaxStats.Energy);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MAXPOWER5, 0);

        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_LEVEL, character.Level);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_FACTIONTEMPLATE, (int)character.Race);

        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BYTES_0, BitConverter.ToUInt32(new[]
        {
            (byte) character.Race,
            (byte) character.Class,
            (byte) character.Gender,
            (byte) character.Class.GetManaType()
        }));

        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_STAT0, character.Attributes.Strength);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_STAT1, character.Attributes.Agility);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_STAT2, character.Attributes.Stamina);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_STAT3, character.Attributes.Intellect);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_STAT4, character.Attributes.Spirit);

        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_RESISTANCES, character.Resistances.Armor);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_RESISTANCES_01, 0); // unknown
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_RESISTANCES_02, character.Resistances.Fire);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_RESISTANCES_03, character.Resistances.Nature);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_RESISTANCES_04, character.Resistances.Frost);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_RESISTANCES_05, character.Resistances.Shadow);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_RESISTANCES_06, character.Resistances.Arcane);

        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_FLAGS, character.Flag);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BASE_MANA, character.Stats.Mana);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BASE_HEALTH, character.Stats.Life);

        var model = character.Race.GetModel(character.Gender);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_DISPLAYID, model);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_NATIVEDISPLAYID, model);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_MOUNTDISPLAYID, 0);

        this.SetUpdateField((int)PlayerFields_Vanilla.PLAYER_FLAGS, 8); // 8 = <GM> Flag

        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BYTES_1, (byte)character.StandState);
        this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BYTES_2, (byte)this.SheathState); // SHEATHED??

        //this.SetUpdateField((int)UnitFields_Vanilla.UNIT_FIELD_BYTES_2, BitConverter.ToUInt16(new[]
        //{
        //    (byte)this.SheathState, // SheathState
        //    (byte)0x01, // UnitBytes2_Flags
        //}));

        this.SetUpdateField((int)PlayerFields_Vanilla.PLAYER_BYTES, BitConverter.ToUInt32(new[]
        {
            character.Skin,
            character.Face,
            character.HairStyle,
            character.HairColor
        }));

        this.SetUpdateField((int)PlayerFields_Vanilla.PLAYER_BYTES_2, BitConverter.ToUInt32(new byte[]
        {
            character.FacialHair,
            0,
            0,
            (byte) character.RestedState
        }));

        this.SetUpdateField((int)PlayerFields_Vanilla.PLAYER_BYTES_3, (uint)character.Gender);
        this.SetUpdateField((int)PlayerFields_Vanilla.PLAYER_XP, 0);
        this.SetUpdateField((int)PlayerFields_Vanilla.PLAYER_NEXT_LEVEL_XP, 400);
        this.SetUpdateField((int)PlayerFields_Vanilla.PLAYER_FIELD_WATCHED_FACTION_INDEX, character.WatchFaction);

        this.SkillGenerate_Vanilla(character);
    }

    private void SetUpdateField_TBC(Character character)
    {
        // UnitField.HEALTH,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_HEALTH, character.Stats.Life);

        // UnitField.POWER1-5,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_POWER1, character.Stats.Mana);
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_POWER2, character.Stats.Rage);
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_POWER3, character.Stats.Focus);
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_POWER4, character.Stats.Energy);
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_POWER5, 0);

        // UnitField.MAXHEALTH,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MAXHEALTH, character.MaxStats.Life);

        // UnitField.MAXPOWER1-5,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MAXPOWER1, character.MaxStats.Mana);
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MAXPOWER2, character.MaxStats.Rage);
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MAXPOWER3, character.MaxStats.Focus);
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MAXPOWER4, character.MaxStats.Energy);
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MAXPOWER5, 0);

        // UnitField.LEVEL,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_LEVEL, character.Level);

        // UnitField.FACTIONTEMPLATE,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_FACTIONTEMPLATE, (int)character.Race);

        // UnitField.BYTES_0,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_BYTES_0, BitConverter.ToUInt32(new[]
        {
            (byte) character.Race,
            (byte) character.Class,
            (byte) character.Gender,
            (byte) character.Class.GetManaType()
        }));

        // UnitField.FLAGS,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_FLAGS, (int)character.Flag);

        // UnitField.BASEATTACKTIME,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_BASEATTACKTIME, 1);

        // UnitField.BOUNDINGRADIUS,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_BOUNDINGRADIUS, 1f);

        // UnitField.COMBATREACH,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_COMBATREACH, 1f);

        var model = character.Race.GetModel(character.Gender);

        // UnitField.DISPLAYID,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_DISPLAYID, model);

        // UnitField.NATIVEDISPLAYID,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_NATIVEDISPLAYID, model);

        // UnitField.MOUNTDISPLAYID,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MOUNTDISPLAYID, 0);

        // UnitField.MINDAMAGE,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MINDAMAGE, 1f);

        // UnitField.MAXDAMAGE,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MAXDAMAGE, 2f);

        // UnitField.MINOFFHANDDAMAGE,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MINOFFHANDDAMAGE, 1f);

        // UnitField.MAXOFFHANDDAMAGE,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MAXOFFHANDDAMAGE, 1f);

        // UnitField.BYTES_1,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_BYTES_1, (byte)character.StandState);

        // UnitField.MOD_CAST_SPEED,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_MOD_CAST_SPEED, 1f);

        // UnitField.STAT0-4,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_STAT0, character.Attributes.Strength);
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_STAT1, character.Attributes.Agility);
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_STAT2, character.Attributes.Stamina);
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_STAT3, character.Attributes.Intellect);
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_STAT4, character.Attributes.Spirit);

        // UnitField.RESISTANCES,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_RESISTANCES, BitConverter.ToUInt32(new[]
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
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_BASE_MANA, character.Stats.Mana);

        // UnitField.BYTES_2,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_BYTES_2, (byte)0);

        // UnitField.ATTACK_POWER,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_ATTACK_POWER, 1);

        // UnitField.ATTACK_POWER_MODS,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_ATTACK_POWER_MODS, 1);

        // UnitField.RANGED_ATTACK_POWER,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_RANGED_ATTACK_POWER, 1);

        // UnitField.RANGED_ATTACK_POWER_MODS,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_RANGED_ATTACK_POWER_MODS, 1);

        // UnitField.MINRANGEDDAMAGE,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MINRANGEDDAMAGE, 1f);

        // UnitField.MAXRANGEDDAMAGE,
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_MAXRANGEDDAMAGE, 1f);

        // PlayerField.FLAGS,
        this.SetUpdateField((int)PlayerFields_TBC.PLAYER_FLAGS, 0x00000008); // FLAG <GM>

        // PlayerField.BYTES_1,
        this.SetUpdateField((int)PlayerFields_TBC.PLAYER_BYTES, BitConverter.ToUInt32(new[]
        {
            character.Skin,
            character.Face,
            character.HairStyle,
            character.HairColor
        }));

        // PlayerField.BYTES_2,
        this.SetUpdateField((int)PlayerFields_TBC.PLAYER_BYTES_2, BitConverter.ToUInt32(new byte[]
        {
            character.FacialHair,
            0,
            0, // Bankbag Slots
            (byte) character.RestedState
        }));

        // PlayerField.BYTES_3,
        this.SetUpdateField((int)PlayerFields_TBC.PLAYER_BYTES_3, BitConverter.ToUInt32(new byte[]
        {
            (byte)character.Gender,
            0, // Drunkness
            0,
            0, // PvP Rank?
        }));

        // PlayerField.XP,
        this.SetUpdateField((int)PlayerFields_TBC.PLAYER_XP, 0);

        // PlayerField.NEXT_LEVEL_XP,
        this.SetUpdateField((int)PlayerFields_TBC.PLAYER_NEXT_LEVEL_XP, 400);

        // PlayerField.CHARACTER_POINTS1,
        this.SetUpdateField((int)PlayerFields_TBC.PLAYER_CHARACTER_POINTS1, 1);

        // PlayerField.CHARACTER_POINTS2,
        this.SetUpdateField((int)PlayerFields_TBC.PLAYER_CHARACTER_POINTS2, 1);

        // PlayerField.BLOCK_PERCENTAGE,
        this.SetUpdateField((int)PlayerFields_TBC.PLAYER_BLOCK_PERCENTAGE, 1f);

        // PlayerField.DODGE_PERCENTAGE,
        this.SetUpdateField((int)PlayerFields_TBC.PLAYER_DODGE_PERCENTAGE, 1f);

        // PlayerField.PARRY_PERCENTAGE,
        this.SetUpdateField((int)PlayerFields_TBC.PLAYER_PARRY_PERCENTAGE, 1f);

        // PlayerField.CRIT_PERCENTAGE,
        this.SetUpdateField((int)PlayerFields_TBC.PLAYER_CRIT_PERCENTAGE, 1f);

        // PlayerField.REST_STATE_EXPERIENCE,
        this.SetUpdateField((int)PlayerFields_TBC.PLAYER_REST_STATE_EXPERIENCE, 1);

        // PlayerField.COINAGE
        this.SetUpdateField((int)PlayerFields_TBC.PLAYER_FIELD_COINAGE, 1);

        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_TARGET, (ulong)0);
        this.SetUpdateField((int)UnitFields_TBC.UNIT_FIELD_BASE_HEALTH, character.Stats.Life);
        this.SetUpdateField((int)PlayerFields_TBC.PLAYER_FIELD_WATCHED_FACTION_INDEX, character.WatchFaction);

        this.SkillGenerate_TBC(character);
    }

    private void SkillGenerate_Vanilla(Character character)
    {
        var a = 0;
        foreach (var skill in character.Skills)
        {
            this.SetUpdateField((int)PlayerFields_Vanilla.PLAYER_SKILL_INFO_1_1 + a * 3, skill.Id);
            this.SetUpdateField((int)PlayerFields_Vanilla.PLAYER_SKILL_INFO_1_1 + a * 3 + 1, skill.Current + (skill.Max << 16));
            a++;
        }
    }

    private void SkillGenerate_TBC(Character character)
    {
        var a = 0;
        foreach (var skill in character.Skills)
        {
            this.SetUpdateField((int)PlayerFields_TBC.PLAYER_SKILL_INFO_1_1 + a * 3, skill.Id);
            this.SetUpdateField((int)PlayerFields_TBC.PLAYER_SKILL_INFO_1_1 + a * 3 + 1, skill.Current + (skill.Max << 16));
            a++;
        }
    }
}
