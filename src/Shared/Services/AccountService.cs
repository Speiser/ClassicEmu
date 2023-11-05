using System;
using System.Threading.Tasks;
using Classic.Shared.Data;
using Dapper;
using LiteDB;

namespace Classic.Shared.Services;

public class AccountService
{
    private readonly ILiteCollection<AccountSession> sessions;
    private readonly AuthDatabase authDatabase;

    public AccountService(ILiteDatabase db, AuthDatabase authDatabase)
    {
        this.sessions = db.GetCollection<AccountSession>("accountSessions");
        this.authDatabase = authDatabase;
    }

    public async Task<PAccount> GetAccount(string username)
    {
        using var connection = this.authDatabase.GetConnection();
        return await connection.QueryFirstOrDefaultAsync<PAccount>("SELECT * FROM accounts WHERE username = @Username;", new { Username = username });
    }

    public async Task AddAccount(PAccount account)
    {
        using var connection = this.authDatabase.GetConnection();
        await connection.ExecuteAsync("INSERT INTO accounts (username, session_key) VALUES (@Username, @SessionKey);", new
        {
            account.Username,
            account.SessionKey,
        });
    }

    public async Task SetSessionKey(PAccount account)
    {
        using var connection = this.authDatabase.GetConnection();
        await connection.ExecuteAsync("UPDATE accounts SET session_key = @SessionKey WHERE username = @Username;", new
        {
            account.SessionKey,
            account.Username,
        });
    }

    [Obsolete("Use PGetAccount().SessionKey")]
    public AccountSession GetSession(string identifier) => this.sessions.FindOne(x => x.Identifier == identifier);

    public bool DeleteSession(string identifier)
    {
        var session = this.GetSession(identifier);
        return session is not null && this.sessions.Delete(session.Id);
    }

    public void ClearAccountSessions() => this.sessions.DeleteAll();
}
