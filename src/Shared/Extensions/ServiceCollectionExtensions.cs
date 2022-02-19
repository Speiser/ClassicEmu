using Classic.Shared.Services;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;

namespace Classic.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        var db = new LiteDatabase(new ConnectionString
        {
            Filename = Configuration.AccountDatabase,
            Connection = ConnectionType.Shared,
        });

        return services
            .AddSingleton(_ => new AccountService(db))
            .AddSingleton(_ => new RealmlistService(db));
    }
}
