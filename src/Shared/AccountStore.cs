using Classic.Shared.Data.Repositories;
using LiteDB;

namespace Classic.Shared
{
    // TODO: find a nice way to get rid of this and static repositories...
    public class AccountStore
    {
        private static readonly ILiteDatabase db = new LiteDatabase(new ConnectionString
        {
            Filename = Configuration.AccountDatabase,
            Connection = ConnectionType.Shared,
        });

        public static AccountRepository AccountRepository { get; } = new AccountRepository(db);
        public static AccountSessionRepository AccountSessionRepository { get; } = new AccountSessionRepository(db);
    }
}
