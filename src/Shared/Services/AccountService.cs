using System;
using System.Threading.Tasks;
using Classic.Shared.Data;
using Dapper;
using LiteDB;

namespace Classic.Shared.Services;

public class AccountService
{
    private readonly ILiteCollection<Account> accounts;
    private readonly ILiteCollection<AccountSession> sessions;
    private readonly AuthDatabase authDatabase;

    public AccountService(ILiteDatabase db, AuthDatabase authDatabase)
    {
        this.accounts = db.GetCollection<Account>("accounts");
        this.sessions = db.GetCollection<AccountSession>("accountSessions");
        this.authDatabase = authDatabase;
    }

    [Obsolete("Use PGetAccount")]
    public Account GetAccount(string identifier) => this.accounts.FindOne(x => x.Identifier == identifier);

    public async Task<PAccount> PGetAccount(string username)
    {
        using var connection = this.authDatabase.GetConnection();
        return await connection.QueryFirstOrDefaultAsync<PAccount>("SELECT * FROM accounts WHERE username = @Username;", new { Username = username });
    }

    public async Task PAddAccount(PAccount account)
    {
        using var connection = this.authDatabase.GetConnection();
        await connection.ExecuteAsync("INSERT INTO accounts (username, session_key) VALUES (@Username, @SessionKey);", new
        {
            account.Username,
            account.SessionKey,
        });
    }

    public async Task PSetSessionKey(PAccount account)
    {
        using var connection = this.authDatabase.GetConnection();
        await connection.ExecuteAsync("UPDATE accounts SET session_key = @SessionKey WHERE username = @Username;", new
        {
            account.SessionKey,
            account.Username,
        });
    }

    public void UpdateAccount(Account account) => this.accounts.Update(account);

    [Obsolete("Use PGetAccount().SessionKey")]
    public AccountSession GetSession(string identifier) => this.sessions.FindOne(x => x.Identifier == identifier);

    public bool DeleteSession(string identifier)
    {
        var session = this.GetSession(identifier);
        return session is not null && this.sessions.Delete(session.Id);
    }

    public void ClearAccountSessions() => this.sessions.DeleteAll();
}
