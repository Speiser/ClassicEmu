using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Classic.Shared.Data;
using Classic.Shared.Data.Enums;
using Classic.Shared.Services;
using Classic.World.Data;
using Classic.World.Data.Enums.Character;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Classic.World;

public class WorldServer : BackgroundService
{
    private readonly IServiceProvider services;
    private readonly RealmlistService realmlistService;
    private readonly Realm realmInfo;
    private readonly Timer cacheSaveTimer;

    private readonly VersionServer serverVanilla;
    private readonly VersionServer serverTBC;
    private readonly VersionServer serverWotLK;

    public WorldServer(IServiceProvider services, ILogger<WorldServer> logger, IWorldManager world, RealmlistService realmlistService)
    {
        this.serverVanilla = new VersionServer(new IPEndPoint(IPAddress.Loopback, 13250), ClientBuild.Vanilla, this.ProcessClient, logger);
        this.serverTBC = new VersionServer(new IPEndPoint(IPAddress.Loopback, 13251), ClientBuild.TBC, this.ProcessClient, logger);
        this.serverWotLK = new VersionServer(new IPEndPoint(IPAddress.Loopback, 13252), ClientBuild.WotLK, this.ProcessClient, logger);

        this.services = services;
        this.World = world;

        this.realmlistService = realmlistService;
        this.realmInfo = this.GetRealmInfo();
        
        this.World.Creatures.Add(new Creature { Model = 169, Position = Map.GetStartingPosition(Race.NightElf) });
        var cacheSaveIntervall = TimeSpan.FromSeconds(15);
        this.cacheSaveTimer = new Timer(this.SaveCache, null, cacheSaveIntervall, cacheSaveIntervall);
    }

    public IWorldManager World { get; }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        this.serverVanilla.Stop();
        this.serverTBC.Stop();
        this.serverWotLK.Stop();

        this.cacheSaveTimer.Dispose();
        this.World.StopWorldLoop();
        this.SaveCache();
        this.realmlistService.RemoveRealm(this.realmInfo);
        await base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _ = this.serverVanilla.StartAsync(stoppingToken);
        _ = this.serverTBC.StartAsync(stoppingToken);
        _ = this.serverWotLK.StartAsync(stoppingToken);

        this.realmlistService.AddRealm(this.realmInfo);

        this.World.StartWorldLoop();
    }

    private async Task ProcessClient(TcpClient client, int build)
    {
        var worldClient = services.GetService<WorldClient>();
        this.World.Connections.Add(worldClient);
        await worldClient.Initialize(client, build);
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
            AddressVanilla = "127.0.0.1:13250",
            AddressTBC = "127.0.0.1:13251",
            AddressWotLK = "127.0.0.1:13252",
            Population = 0,
            TimeZone = 1, // 1 seems to be needed for wotlk
        };
    }

    private void SaveCache(object _ = null)
    {
        this.World.CharacterService.Save();
    }
}
