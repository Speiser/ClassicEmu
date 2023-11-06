using System.Data;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace Classic.World.Services;

public class WorldDatabase
{
    private readonly NpgsqlDataSource dataSource;

    public WorldDatabase(string connectionString)
    {
        this.dataSource = NpgsqlDataSource.Create(connectionString);
    }

    public async Task Initialize()
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        using var connection = this.GetConnection();
        var initializeScript = await File.ReadAllTextAsync("Data/SQL/WorldDatabaseBaseScript.sql");
        await connection.ExecuteAsync(initializeScript);
    }

    public IDbConnection GetConnection() => this.dataSource.OpenConnection();

    public void Dispose()
    {
        this.dataSource.Dispose();
    }
}
