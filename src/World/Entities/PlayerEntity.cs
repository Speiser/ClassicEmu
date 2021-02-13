using System;
using Classic.Data;
using Classic.World.Entities.Enums;
using Classic.World.Entities.Utils;
using Classic.World.Extensions;

namespace Classic.World.Entities
{
    public class PlayerEntity : UnitEntity
    {
        public new int Model;

        public PlayerEntity(Character character)
            : base(new ObjectGuid((uint) character.ID, TypeId.TypeidPlayer, HighGuid.HighguidPlayer))
        {
            Character = character;
            Model = character.Race.GetModel(character.Gender);
            Scale = character.Race.GetScale(character.Gender);

            SetUpdateField((int) ObjectFields.Type, (uint) 0x19);
            SetUpdateField((int) ObjectFields.ScaleX, Size);
            SetUpdateField((int) ObjectFields.Padding, 0);

            SetUpdateField((int) UnitFields.UNIT_FIELD_TARGET, (ulong) 0);

            SetUpdateField((int) UnitFields.UNIT_FIELD_HEALTH, character.Stats.Life);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER1, character.Stats.Mana);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER2, character.Stats.Rage);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER3, character.Stats.Focus);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER4, character.Stats.Energy);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER5, 0);

            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXHEALTH, character.MaxStats.Life);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER1, character.MaxStats.Mana);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER2, character.MaxStats.Rage);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER3, character.MaxStats.Focus);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER4, character.MaxStats.Energy);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER5, 0);


            SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, character.Level);
            SetUpdateField((int) UnitFields.UNIT_FIELD_FACTIONTEMPLATE, (int) character.Race);

            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0, BitConverter.ToUInt32(new[]
            {
                (byte) character.Race,
                (byte) character.Class,
                (byte) character.Gender,
                (byte) character.Class.GetManaType()
            }));

            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT0, character.Attributes.Strength);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT1, character.Attributes.Agility);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT2, character.Attributes.Stamina);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT3, character.Attributes.Intellect);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT4, character.Attributes.Spirit);

            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES, character.Resistances.Armor);
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_01, 0); // unknown
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_02, character.Resistances.Fire);
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_03, character.Resistances.Nature);
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_04, character.Resistances.Frost);
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_05, character.Resistances.Shadow);
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_06, character.Resistances.Arcane);

            SetUpdateField((int) UnitFields.UNIT_FIELD_FLAGS, character.Flag);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BASE_MANA, character.Stats.Mana);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BASE_HEALTH, character.Stats.Life);

            SetUpdateField((int) UnitFields.UNIT_FIELD_DISPLAYID, Model);
            SetUpdateField((int) UnitFields.UNIT_FIELD_NATIVEDISPLAYID, Model);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MOUNTDISPLAYID, 0);

            // FLAG <GM>
            SetUpdateField((int) PlayerFields.PLAYER_FLAGS, 0x00000008);

            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_1, (byte) character.StandState);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_2, 0);

            SetUpdateField((int) PlayerFields.PLAYER_BYTES, BitConverter.ToUInt32(new[]
            {
                character.Skin,
                character.Face,
                character.HairStyle,
                character.HairColor
            }));

            SetUpdateField((int) PlayerFields.PLAYER_BYTES_2, BitConverter.ToUInt32(new byte[]
            {
                character.FacialHair,
                0,
                0,
                (byte) character.RestedState
            }));

            SetUpdateField((int) PlayerFields.PLAYER_BYTES_3, (uint) character.Gender);
            SetUpdateField((int) PlayerFields.PLAYER_XP, 0);
            SetUpdateField((int) PlayerFields.PLAYER_NEXT_LEVEL_XP, 400);
            SetUpdateField((int) PlayerFields.PLAYER_SKILL_INFO_1_1, 26); // Needed??
            SetUpdateField((int) PlayerFields.PLAYER_FIELD_WATCHED_FACTION_INDEX, character.WatchFaction);

            SkillGenerate();
        }

        public override int DataLength => (int) PlayerFields.PLAYER_END - 0x4;

        public Character Character { get; }
        public override string Name => Character.Name;
        public ulong TargetId { get; set; }

        private void SkillGenerate()
        {
            var a = 0;
            foreach (var skill in Character.Skills)
            {
                SetUpdateField((int)PlayerFields.PLAYER_SKILL_INFO_1_1 + a * 3, skill.Id);
                SetUpdateField((int)PlayerFields.PLAYER_SKILL_INFO_1_1 + a * 3 + 1, skill.Current + (skill.Max << 16));
                a++;
            }
        }
    }
}