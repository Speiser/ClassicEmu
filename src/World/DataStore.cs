using System.Collections.Concurrent;
using Classic.Shared;
using Classic.World.Data;
using LiteDB;

namespace Classic.World
{
    // TODO: as instance
    public class DataStore
    {
        private static readonly ILiteDatabase database = new LiteDatabase(new ConnectionString
        {
            Filename = Configuration.DatabaseConnectionString,
            Connection = ConnectionType.Shared,
        });

        private static readonly ILiteCollection<Character> characters = database.GetCollection<Character>("characters");

        public static Character GetCharacter(ulong charId)
        {
            return characters.FindOne(c => c.Id == charId);
        }

        public static bool AddCharacter(Character character)
        {
            if (GetCharacter(character.Id) is not null)
            {
                return false;
            }

            characters.Insert(character);
            return true;
        }

        public static bool DeleteCharacter(ulong charId)
        {
            return characters.Delete(charId);
        }
    }
}
