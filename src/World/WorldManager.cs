using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Classic.Shared.Data;
using Classic.World.Data;
using Classic.World.Packets.Server;
using Classic.World.Services;

namespace Classic.World
{
    public class WorldManager : IWorldManager
    {
        public WorldManager(CharacterService characterService)
        {
            this.CharacterService = characterService;
        }

        public List<WorldClient> Connections { get; } = new List<WorldClient>();
        public List<Creature> Creatures { get; } = new List<Creature>();
        public CharacterService CharacterService { get; }

        public async Task SpawnPlayer(Character character, int build)
        {
            var updateForOtherActivePlayers = new Dictionary<int, SMSG_UPDATE_OBJECT>
            {
                { ClientBuild.Vanilla, SMSG_UPDATE_OBJECT.CreatePlayer(character, ClientBuild.Vanilla) },
                { ClientBuild.TBC, SMSG_UPDATE_OBJECT.CreatePlayer(character, ClientBuild.TBC) },
            };

            var _this = this.Connections.Single(x => x.CharacterId == character.Id);

            foreach (var other in this.Connections)
            {
                if (!other.IsInWorld) continue;
                if (other.CharacterId == character.Id) continue;

                var otherCharacter = this.CharacterService.GetCharacter(other.CharacterId);

                if (!IsInRange(character, otherCharacter)) continue;

                await _this.SendPacket(SMSG_UPDATE_OBJECT.CreatePlayer(otherCharacter, build));
                await other.SendPacket(updateForOtherActivePlayers[other.Build]);
            }
        }

        public async Task SpawnCreature(Creature creature)
        {
            this.Creatures.Add(creature);

            foreach (var connection in this.Connections)
            {
                var character = this.CharacterService.GetCharacter(connection.CharacterId);
                if (character is null) continue;

                if (!IsInRange(character, creature)) continue;

                await connection.SendPacket(SMSG_UPDATE_OBJECT_VANILLA.CreateUnit(creature));
            }
        }

        // Pseudo "range check"
        private static bool IsInRange(IHasPosition a, IHasPosition b) => a.Position.Zone == b.Position.Zone;
    }
}
