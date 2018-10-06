using System.Collections.Concurrent;

namespace Classic.Common
{
    // TODO: Replace with sqlite db
    public class DataStore
    {
        public static ConcurrentDictionary<string, User> Users { get; } = new ConcurrentDictionary<string, User>();
    }
}
