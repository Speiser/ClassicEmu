using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Classic.Common;
using Classic.Data;
using Classic.World.Messages.Server;

namespace Classic.World
{
    public class WorldState
    {
        public List<WorldClient> Connections { get; } = new List<WorldClient>();
        public List<Creature> Creatures { get; } = new List<Creature>();

        public async Task SpawnPlayer(Character character, int build)
        {
            var updateForOtherActivePlayers = new Dictionary<int, SMSG_UPDATE_OBJECT>
            {
                { ClientBuild.Vanilla, SMSG_UPDATE_OBJECT.CreatePlayer(character, ClientBuild.Vanilla) },
                { ClientBuild.TBC, SMSG_UPDATE_OBJECT.CreatePlayer(character, ClientBuild.TBC) },
            };

            var _this = this.Connections.Single(x => x.Character?.ID == character.ID);

            foreach (var other in this.Connections)
            {
                if (other.Character is null) continue;
                if (other.Character.ID == character.ID) continue;
                
                if (!IsInRange(character, other.Character)) continue;

                await _this.SendPacket(SMSG_UPDATE_OBJECT.CreatePlayer(other.Character, build));
                await other.SendPacket(updateForOtherActivePlayers[other.Build]);
            }
        }

        public async Task SpawnCreature(Creature creature)
        {
            this.Creatures.Add(creature);

            foreach (var connection in this.Connections)
            {
                if (connection.Character is null) continue;

                if (!IsInRange(connection.Character, creature)) continue;

                await connection.SendPacket(SMSG_UPDATE_OBJECT_VANILLA.CreateUnit(creature));
            }
        }

        // Pseudo "range check"
        private static bool IsInRange(IHasPosition a, IHasPosition b) => a.Position.Zone == b.Position.Zone;
    }
}
