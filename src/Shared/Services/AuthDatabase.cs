using System.IO;
using System.Threading.Tasks;

namespace Classic.Shared.Services;

public class AuthDatabase
{
    public AuthDatabase(string connectionString)
    {

    }

    public async Task Initialize()
    {
        var initializeScript = await File.ReadAllTextAsync("Data/SQL/AuthDatabaseBaseScript.sql");
    }
}