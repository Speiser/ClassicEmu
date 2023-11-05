using System.Threading.Tasks;
using Classic.Shared.Data;
using Dapper;

namespace Classic.Shared.Services;

// TODO: Maybe add a Delete and DeleteAll sessions?
public class AccountService
{
    private readonly AuthDatabase authDatabase;

    public AccountService(AuthDatabase authDatabase)
    {
        this.authDatabase = authDatabase;
    }

    public async Task<Account> GetAccount(string username)
    {
        using var connection = this.authDatabase.GetConnection();
        return await connection.QueryFirstOrDefaultAsync<Account>("SELECT * FROM accounts WHERE username = @Username;", new { Username = username });
    }

    public async Task AddAccount(Account account)
    {
        using var connection = this.authDatabase.GetConnection();
        await connection.ExecuteAsync("INSERT INTO accounts (username, session_key) VALUES (@Username, @SessionKey);", new
        {
            account.Username,
            account.SessionKey,
        });
    }

    public async Task SetSessionKey(Account account)
    {
        using var connection = this.authDatabase.GetConnection();
        await connection.ExecuteAsync("UPDATE accounts SET session_key = @SessionKey WHERE username = @Username;", new
        {
            account.SessionKey,
            account.Username,
        });
    }
}
