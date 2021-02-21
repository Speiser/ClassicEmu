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

        // This is a hack, since I need to figure out the Client Build before sending the first message from the world client
        // TODO: Use IP + port as key
        public static ConcurrentDictionary<int, int> PortToClientBuild { get; } = new ConcurrentDictionary<int, int>();

        public static Character GetCharacter(ulong charID) // TODO find a better way...
            => Users
                .SelectMany(x => x.Value.Characters)
                .Where(c => c.ID == charID)
                .FirstOrDefault();
    }
}
