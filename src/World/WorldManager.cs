using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Classic.Shared.Data;
using Classic.World.Data;
using Classic.World.Packets.Server;
using Classic.World.Services;

namespace Classic.World;

public class WorldManager : IWorldManager
{
    private const int SleepConst = 100;
    private bool isStopped;

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

            var otherCharacter = await this.CharacterService.GetCharacter(other.CharacterId);

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
            var character = await this.CharacterService.GetCharacter(connection.CharacterId);
            if (character is null) continue;

            if (!IsInRange(character, creature)) continue;

            await connection.SendPacket(SMSG_UPDATE_OBJECT_VANILLA.CreateUnit(creature));
        }
    }

    public void StartWorldLoop()
    {
        Task.Run(async () =>
        {
            var lastUpdateFinished = DateTime.Now;

            while (!this.isStopped)
            {
                var beforeCurrent = DateTime.Now;
                var dt = GetDeltaTime(lastUpdateFinished, beforeCurrent);
                await this.Update(dt);
                lastUpdateFinished = beforeCurrent;
                var executionDeltaTime = GetDeltaTime(beforeCurrent, DateTime.Now);
                if (executionDeltaTime < SleepConst)
                {
                    await Task.Delay(SleepConst - (int)executionDeltaTime);
                }
            }
        });
    }

    public void StopWorldLoop()
    {
        this.isStopped = true;
    }

    private async Task Update(double dt)
    {
        foreach (var client in this.Connections)
        {
            if (!client.IsInWorld)
            {
                continue;
            }

            await client.Update(dt);

            if (client.Build == ClientBuild.Vanilla)
            {
                await client.SendPacket(SMSG_UPDATE_OBJECT_VANILLA.UpdateValues(this.Creatures));
            }
        }
    }

    private static double GetDeltaTime(DateTime prev, DateTime curr) => (curr - prev).TotalMilliseconds;

    // Pseudo "range check"
    private static bool IsInRange(IHasPosition a, IHasPosition b) => a.Position.Zone == b.Position.Zone;
}
