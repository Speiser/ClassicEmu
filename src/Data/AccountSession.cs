using Classic.Common;

namespace Classic.Data
{
    public class AccountSession
    {
        public AccountSession(string identifier, byte[] sessionKey)
        {
            if (!DataStore.Accounts.TryGetValue(identifier, out var account))
            {
                // Create new (for development)
                account = new Account { Identifier = identifier };
                DataStore.Accounts.TryAdd(identifier, account);
            }

            this.SessionKey = sessionKey;
            this.Account = account;
        }

        public byte[] SessionKey { get; }
        public Account Account { get; }
    }
}
