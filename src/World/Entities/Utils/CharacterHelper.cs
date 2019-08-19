using Classic.Data.Enums.Character;
using Classic.World.Entities.Enums;

namespace Classic.World.Entities.Utils
{
    internal class CharacterHelper
    {
        internal static ManaTypes GetClassManaType(Classes classe)
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
}