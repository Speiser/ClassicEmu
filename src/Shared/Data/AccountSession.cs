using LiteDB;

namespace Classic.Shared.Data
{
    public class AccountSession
    {
        public AccountSession() { }
        public AccountSession(Account account, byte[] sessionKey)
        {
            this.SessionKey = sessionKey;
            this.Account = account;
        }

        public ObjectId Id { get; set; }
        public string Identifier => this.Account.Identifier;
        public byte[] SessionKey { get; set; }
        public Account Account { get; set; }
    }
}
