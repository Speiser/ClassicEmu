using System.Collections.Generic;
using Classic.Data;

namespace Classic.World
{
    public class WorldState
    {
        public List<WorldClient> Connections { get; } = new List<WorldClient>();
        public List<Creature> CurrentCreatures { get; } = new List<Creature>();
    }
}
