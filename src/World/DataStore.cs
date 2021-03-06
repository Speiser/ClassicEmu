using Classic.Shared;
using Classic.World.Data.Repositories;
using LiteDB;

namespace Classic.World
{
    // TODO: as instance
    public class DataStore
    {
        private static readonly ILiteDatabase db = new LiteDatabase(new ConnectionString
        {
            Filename = Configuration.RealmDatabase,
            Connection = ConnectionType.Direct,
        });

        public static CharacterRepository CharacterRepository { get; } = new CharacterRepository(db);
    }
}
