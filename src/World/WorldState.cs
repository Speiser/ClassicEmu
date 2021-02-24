using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Classic.Data;
using Classic.World.Messages.Server;

namespace Classic.World
{
    public class WorldState
    {
        public List<WorldClient> Connections { get; } = new List<WorldClient>();
        public List<Creature> Creatures { get; } = new List<Creature>();

        public async Task SpawnPlayer(Character character)
        {
            var updateForOtherActivePlayers = SMSG_UPDATE_OBJECT_VANILLA.CreatePlayer(character);
            var _this = this.Connections.Single(x => x.Character?.ID == character.ID);

            foreach (var other in this.Connections)
            {
                // TODO: Add range check
                if (other.Character is null) continue;
                if (other.Character.ID == character.ID) continue; // Should not happen?
                await _this.SendPacket(SMSG_UPDATE_OBJECT_VANILLA.CreatePlayer(other.Character));
                await other.SendPacket(updateForOtherActivePlayers);
            }
        }

        public async Task SpawnCreature(Creature creature)
        {
            this.Creatures.Add(creature);

            foreach (var connection in this.Connections)
            {
                // TODO: Add range check
                if (connection.Character is null) continue;
                await connection.SendPacket(SMSG_UPDATE_OBJECT_VANILLA.CreateUnit(creature));
            }
        }
    }
}
