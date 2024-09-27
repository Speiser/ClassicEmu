using System.Collections.Generic;
using Classic.World.Data;
using Classic.World.Data.Enums.Character;
using Classic.World.Data.Enums.Map;
using Classic.World.Packets.Client;

namespace Classic.World;

public class CharacterFactory
{
    public static PCharacter Create(CMSG_CHAR_CREATE request)
    {
        var position = GetStartingPosition(request.Race, request.Class);

        return new PCharacter
        {
            Class = (short)request.Class,
            Face = request.Face,
            FacialHair = request.FacialHair,
            Gender = (short)request.Gender,
            HairColor = request.HairColor,
            HairStyle = request.HairStyle,
            Name = request.Name,
            OutfitId = request.OutfitId,
            PositionX = position.X,
            PositionY = position.Y,
            PositionZ = position.Z,
            PositionO = position.Orientation,
            MapId = (int)position.ID,
            ZoneId = (int)position.Zone,
            Race = (short)request.Race,
            Skin = request.Skin,
            Flag = (int)CharacterFlag.None,
            StandState = (byte)StandState.Stand,
        };
    }

    private static Map GetStartingPosition(Race race, Classes _class)
    {
        return _class == Classes.DeathKnight
            ? new Map { X = 2355.84f, Y = -5664.77f, Z = 426.028f, Orientation = 3.65997f, ID = (MapID)609, Zone = (ZoneID)4298 }
            : Map.GetStartingPosition(race);
    }

    public static List<Spell> GetInitialSpells(Race race, Classes _class)
    {
        return [Spell.Fireball(), Spell.Pyroblast(), Spell.ArcaneMissiles()];
    }

    public static List<Skill> GetInitialSkills(Race race, Classes _class)
    {
        return [Skill.Staves()];
    }

    public static ActionBarItem[] GetInitialActionBar(Race race, Classes _class)
    {
        var actionBar = new ActionBarItem[120];
        actionBar[0] = ActionBarItem.Fireball();
        actionBar[1] = ActionBarItem.Pyroblast();
        actionBar[2] = ActionBarItem.ArcaneMissiles();
        return actionBar;
    }
}
