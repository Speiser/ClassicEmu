using System.Collections.Generic;
using Classic.World.Data;
using Classic.World.Data.Enums.Character;
using Classic.World.Data.Enums.Map;
using Classic.World.Packets.Client;

namespace Classic.World
{
    public class CharacterFactory
    {
        public static Character Create(CMSG_CHAR_CREATE request)
        {
            var position = GetStartingPosition(request.Race, request.Class);
            // GetInitialItems
            var spells = GetInitialSpells(request.Race, request.Class);
            var skills = GetInitialSkills(request.Race, request.Class);
            var actionBar = GetInitialActionBar(request.Race, request.Class);

            return new Character
            {
                ActionBar = actionBar,
                Class = request.Class,
                Face = request.Face,
                FacialHair = request.FacialHair,
                Gender = request.Gender,
                HairColor = request.HairColor,
                HairStyle = request.HairStyle,
                Level = 1,
                Name = request.Name,
                OutfitId = request.OutfitId,
                Position = position,
                Race = request.Race,
                Skills = skills,
                Skin = request.Skin,
                Spells = spells,
            };
        }

        private static Map GetStartingPosition(Race race, Classes _class)
        {
            return _class == Classes.DeathKnight
                ? new Map { X = 2355.84f, Y = -5664.77f, Z = 426.028f, Orientation = 3.65997f, ID = (MapID)609, Zone = (ZoneID)4298 }
                : Map.GetStartingPosition(race);
        }

        private static List<Spell> GetInitialSpells(Race race, Classes _class)
        {
            return new List<Spell> { Spell.Fireball() };
        }

        private static List<Skill> GetInitialSkills(Race race, Classes _class)
        {
            return new List<Skill> { Skill.Staves() };
        }

        private static ActionBarItem[] GetInitialActionBar(Race race, Classes _class)
        {
            var actionBar = new ActionBarItem[120];
            actionBar[0] = ActionBarItem.Fireball();
            return actionBar;
        }
    }
}
