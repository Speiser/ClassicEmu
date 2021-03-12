using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Classic.Shared;
using Classic.Shared.Data;
using Classic.Shared.Data.Enums;
using Classic.Shared.Services;
using Classic.World.Data;
using Classic.World.Data.Enums.Character;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Classic.World
{
    public class WorldServer : ServerBase
    {
        private readonly IServiceProvider services;
        private readonly RealmlistService realmlistService;
        private readonly Realm realmInfo;
        private readonly Timer cacheSaveTimer;

        public WorldServer(IServiceProvider services, ILogger<WorldServer> logger, IWorldManager world, RealmlistService realmlistService)
            : base(new IPEndPoint(IPAddress.Loopback, 13250), logger)
        {
            this.services = services;
            this.World = world;

            this.realmlistService = realmlistService;
            this.realmInfo = this.GetRealmInfo();
            
            this.World.Creatures.Add(new Creature { Model = 169, Position = Map.GetStartingPosition(Race.NightElf) });
            var cacheSaveIntervall = TimeSpan.FromSeconds(15);
            this.cacheSaveTimer = new Timer(this.SaveCache, null, cacheSaveIntervall, cacheSaveIntervall);
        }

        public IWorldManager World { get; }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            this.realmlistService.AddRealm(this.realmInfo);
            return base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            this.cacheSaveTimer.Dispose();
            this.SaveCache();
            this.realmlistService.RemoveRealm(this.realmInfo);
            await base.StopAsync(cancellationToken);
        }

        protected override async Task ProcessClient(TcpClient client)
        {
            var worldClient = services.GetService<WorldClient>();
            this.World.Connections.Add(worldClient);
            await worldClient.Initialize(client);
        }

        private Realm GetRealmInfo()
        {
            // TODO: From config
            return new Realm
            {
                Type = RealmType.PVP,
                Lock = 0,
                Flags = RealmFlag.None,
                Name = "TestServer",
                Address = "127.0.0.1:13250",
                Population = 0,
                TimeZone = 1, // 1 seems to be needed for wotlk
            };
        }

        private void SaveCache(object _ = null)
        {
            this.World.CharacterService.Save();
        }
    }
}
