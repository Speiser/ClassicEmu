using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace Classic.Shared.Services;

// TODO: Figure out how to handle SMALLINT -> byte conversion as postgres doesn't support TINYINT :(
// TODO: Where to do CRUD operations? Here, service, API????
public class AuthDatabase : IDisposable
{
    private readonly NpgsqlDataSource dataSource;

    public AuthDatabase(string connectionString)
    {
        this.dataSource = NpgsqlDataSource.Create(connectionString);
    }

    public async Task Initialize()
    {
        using var connection = this.GetConnection();
        var initializeScript = await File.ReadAllTextAsync("Data/SQL/AuthDatabaseBaseScript.sql");
        await connection.ExecuteAsync(initializeScript);
    }

    public IDbConnection GetConnection() => this.dataSource.OpenConnection();

    public void Dispose()
    {
        this.dataSource.Dispose();
    }
}