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
using Classic.World.Services;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Classic.World;

public class WorldServer : BackgroundService
{
    private readonly IServiceProvider services;
    private readonly ILogger<WorldServer> logger;
    private readonly RealmlistService realmlistService;
    private readonly PRealm realmInfo;
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
        this.logger = logger;
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
        await this.realmlistService.RemoveRealm(this.realmInfo.Name);
        await base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _ = this.serverVanilla.StartAsync(stoppingToken);
        _ = this.serverTBC.StartAsync(stoppingToken);
        _ = this.serverWotLK.StartAsync(stoppingToken);

        var authDb = this.services.GetService<AuthDatabase>();
        using var connection = authDb.GetConnection();

        try
        {
            // Has to match DB name from world connection string
            await connection.ExecuteAsync($"CREATE DATABASE classicemu_world_dev;");
            this.logger.LogInformation("Created database 'classicemu_world_dev'");
        }
        catch (PostgresException e) when (e.SqlState == "42P04")
        {
            // 42P04: database already exists
            this.logger.LogInformation("Database 'classicemu_world_dev' already exists");
        }

        await this.services.GetService<WorldDatabase>().Initialize();

        await this.realmlistService.AddRealm(this.realmInfo);

        this.World.StartWorldLoop();
    }

    private async Task ProcessClient(TcpClient client, int build)
    {
        var worldClient = services.GetService<WorldClient>();
        this.World.Connections.Add(worldClient);
        await worldClient.Initialize(client, build);
    }

    private PRealm GetRealmInfo()
    {
        // TODO: From config
        return new PRealm
        {
            Type = (byte)RealmType.PVP,
            Flags = (byte)RealmFlag.None,
            Name = "Hello World",
            Address = "127.0.0.1",
            PortVanilla = 13250,
            PortTbc = 13251,
            PortWotlk = 13252,
            Population = 0,
            Timezone = 1, // 1 seems to be needed for wotlk
        };
    }

    private void SaveCache(object _ = null)
    {
        // TODO: Rethink this
        //this.World.CharacterService.Save();
    }
}
