using System.Collections.Generic;
using System.Threading.Tasks;
using Classic.World.Data;
using Classic.World.Services;

namespace Classic.World;

public interface IWorldManager
{
    List<WorldClient> Connections { get; }
    List<Creature> Creatures { get; } // TODO...
    CharacterService CharacterService { get; }
    Task SpawnPlayer(Character character, int build);
    Task SpawnCreature(Creature creature);
}
