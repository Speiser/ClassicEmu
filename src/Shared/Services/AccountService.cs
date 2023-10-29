using Classic.Shared.Data;
using LiteDB;

namespace Classic.Shared.Services;

public class AccountService
{
    private readonly ILiteCollection<Account> accounts;
    private readonly ILiteCollection<AccountSession> sessions;

    public AccountService(ILiteDatabase db)
    {
        this.accounts = db.GetCollection<Account>("accounts");
        this.sessions = db.GetCollection<AccountSession>("accountSessions");
    }

    public Account GetAccount(string identifier) => this.accounts.FindOne(x => x.Identifier == identifier);

    public void AddAccount(Account account)
    {
        this.accounts.Insert(account);
        this.accounts.EnsureIndex(x => x.Identifier);
    }

    public void UpdateAccount(Account account) => this.accounts.Update(account);

    public AccountSession GetSession(string identifier) => this.sessions.FindOne(x => x.Identifier == identifier);

    public void AddSession(AccountSession session)
    {
        this.DeleteSession(session.Identifier);
        this.sessions.Insert(session);
        this.sessions.EnsureIndex(x => x.Identifier);
    }

    public bool DeleteSession(string identifier)
    {
        var session = this.GetSession(identifier);
        return session is not null && this.sessions.Delete(session.Id);
    }

    public void ClearAccountSessions() => this.sessions.DeleteAll();
}
