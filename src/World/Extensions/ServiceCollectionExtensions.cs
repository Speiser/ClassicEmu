using Classic.Shared;
using Classic.World.Cryptography;
using Classic.World.Services;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Classic.World.Extensions
{
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
                .AddTransient<AuthCrypt>()
                .AddTransient<WorldClient>()
                .AddSingleton<IWorldManager, WorldManager>()
                .AddSingleton<WorldPacketHandler>()
                .AddSingleton<WorldServer>()
                .AddHostedService(prov => prov.GetService<WorldServer>());
        }
    }
}
