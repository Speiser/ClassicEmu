using LiteDB;

namespace Classic.Shared.Data.Repositories
{
    public class AccountRepository
    {
        private readonly ILiteCollection<Account> accounts;

        public AccountRepository(ILiteDatabase db)
        {
            this.accounts = db.GetCollection<Account>("accounts");
        }

        public Account GetAccount(string identifier) => this.accounts.FindOne(x => x.Identifier == identifier);

        public void AddAccount(Account account)
        {
            this.accounts.Insert(account);
            this.accounts.EnsureIndex(x => x.Identifier);
        }

        public void UpdateAccount(Account account) => this.accounts.Update(account);
    }
}
