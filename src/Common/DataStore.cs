using System.Collections.Concurrent;
using Classic.Data;

namespace Classic.Common
{
    // TODO: Replace with sqlite db
    public class DataStore
    {
        public static ConcurrentDictionary<string, User> Users { get; } = new ConcurrentDictionary<string, User>();
    }
}
