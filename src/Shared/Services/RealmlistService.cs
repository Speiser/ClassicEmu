using System.Linq;
using System.Threading.Tasks;
using Classic.Shared.Data;
using Classic.Shared.Data.Enums;
using Dapper;

namespace Classic.Shared.Services;

public class RealmlistService
{
    private readonly AuthDatabase authDatabase;

    public RealmlistService(AuthDatabase authDatabase)
    {
        this.authDatabase = authDatabase;
    }

    public async Task RemoveRealm(string realmName)
    {
        using var connection = this.authDatabase.GetConnection();
        await connection.ExecuteAsync("DELETE FROM realms WHERE name = @Name;", new { Name = realmName });
    }

    public async Task Clear()
    {
        using var connection = this.authDatabase.GetConnection();
        await connection.ExecuteAsync("DELETE FROM realms;");
    }

    public async Task<Realm[]> GetRealms()
    {
        using var connection = this.authDatabase.GetConnection();
        return (await connection.QueryAsync<PRealm>("SELECT name, address, port_vanilla as PortVanilla, port_tbc as PortTbc, port_wotlk as PortWotlk, type, flags, population, timezone FROM realms"))
            // TODO: Try to find a nicer way of mapping with Dapper?
            .Select(r => new Realm
            {
                Name = r.Name,
                AddressVanilla = $"{r.Address}:{r.PortVanilla}",
                AddressTBC = $"{r.Address}:{r.PortTbc}",
                AddressWotLK = $"{r.Address}:{r.PortWotlk}",
                Flags = (RealmFlag)r.Flags,
                Lock = 0,
                Population = (uint)r.Population,
                TimeZone = r.Timezone,
                Type = (RealmType)r.Type,
            })
            .ToArray();
    }

    public async Task AddRealm(PRealm realm)
    {
        using var connection = this.authDatabase.GetConnection();
        await connection.ExecuteAsync(@"
INSERT INTO realms
(name, address, port_vanilla, port_tbc, port_wotlk, type, flags, population, timezone)
VALUES
(@Name, @Address, @PortVanilla, @PortTbc, @PortWotlk, @Type, @Flags, @Population, @Timezone);",
        new
        {
            realm.Name,
            realm.Address,
            realm.PortVanilla,
            realm.PortTbc,
            realm.PortWotlk,
            realm.Type,
            realm.Flags,
            realm.Population,
            realm.Timezone,
        });
    }
}
