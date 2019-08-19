using Classic.Data.Enums.Character;
using Classic.World.Entities.Enums;

namespace Classic.World.Extensions
{
    public static class CharacterEnumExtensions
    {
        public static ManaTypes GetManaType(this Classes classe)
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

        public static int GetModel(this Race race, Gender gender)
        {
            switch (race)
            {
                case Race.Human:
                    return 49 + (int)gender;
                case Race.Orc:
                    return 51 + (int)gender;
                case Race.Dwarf:
                    return 53 + (int)gender;
                case Race.NightElf:
                    return 55 + (int)gender;
                case Race.Undead:
                    return 57 + (int)gender;
                case Race.Tauren:
                    return 59 + (int)gender;
                case Race.Gnome:
                    return 1563 + (int)gender;
                case Race.Troll:
                    return 1478 + (int)gender;
            }

            return 16358 + (int)Gender.Male;
        }

        public static float GetScale(this Race race, Gender gender)
        {
            if (race != Race.Tauren) return 1F;

            return gender == Gender.Male ? 1.3F : 1.25F;
        }
    }
}
