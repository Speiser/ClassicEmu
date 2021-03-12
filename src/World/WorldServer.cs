using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Classic.Shared;
using Classic.World.Data;
using Classic.World.Data.Enums.Character;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Classic.World
{
    public class WorldServer : ServerBase
    {
        private readonly IServiceProvider services;
        private readonly Timer cacheSaveTimer;

        public WorldServer(IServiceProvider services, ILogger<WorldServer> logger, IWorldManager world)
            : base(new IPEndPoint(IPAddress.Loopback, 13250), logger)
        {
            this.services = services;
            this.World = world;
            this.World.Creatures.Add(new Creature { Model = 169, Position = Map.GetStartingPosition(Race.NightElf) });
            var cacheSaveIntervall = TimeSpan.FromSeconds(15);
            this.cacheSaveTimer = new Timer(this.SaveCache, null, cacheSaveIntervall, cacheSaveIntervall);
        }

        public IWorldManager World { get; }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            this.cacheSaveTimer.Dispose();
            this.SaveCache();
            await base.StopAsync(cancellationToken);
        }

        protected override async Task ProcessClient(TcpClient client)
        {
            var worldClient = services.GetService<WorldClient>();
            this.World.Connections.Add(worldClient);
            await worldClient.Initialize(client);
        }

        private void SaveCache(object _ = null)
        {
            this.World.CharacterService.Save();
        }
    }
}
