using System.Collections.Concurrent;
using System.Linq;
using Classic.Data;

namespace Classic.Common
{
    // TODO: Replace with sqlite db
    // TODO: as instance
    public class DataStore
    {
        public static ConcurrentDictionary<string, User> Users { get; } = new ConcurrentDictionary<string, User>();
        public static Character GetCharacter(ulong charID) // TODO find a better way...
            => Users
                .SelectMany(x => x.Value.Characters)
                .Where(c => c.ID == charID)
                .SingleOrDefault();
    }
}
