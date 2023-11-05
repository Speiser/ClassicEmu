using Classic.Shared;
using Classic.Shared.Services;
using Classic.World.Packets;
using Classic.World.Services;
using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Classic.World.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWorldServer(this IServiceCollection services)
    {
        var db = new LiteDatabase(new ConnectionString
        {
            Filename = Configuration.RealmDatabase,
            Connection = ConnectionType.Direct,
        });

        return services
            .AddSingleton(prov => new CharacterService(db, prov.GetService<ILogger<CharacterService>>()))
            .AddTransient<WorldClient>()
            .AddSingleton<IWorldManager, WorldManager>()
            .AddSingleton(s => new AuthDatabase(s.GetService<IConfiguration>().GetConnectionString("auth-db")))
            .AddSingleton(s => new WorldDatabase(s.GetService<IConfiguration>().GetConnectionString("world-db")))
            .AddSingleton<WorldPacketHandler>()
            .AddSingleton<WorldServer>()
            .AddHostedService(prov => prov.GetService<WorldServer>());
    }
}
