using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Classic.Data;
using Newtonsoft.Json;

namespace Classic.Common
{
    // TODO: Replace with sqlite db
    // TODO: as instance
    public class DataStore
    {
        private const string AccountsFile = "accounts.json";

        public static ConcurrentDictionary<string, Account> Accounts { get; private set; }

        public static ConcurrentDictionary<string, AccountSession> Sessions { get; } = new ConcurrentDictionary<string, AccountSession>();

        // This is a hack, since I need to figure out the Client Build before sending the first message from the world client
        // TODO: Use IP + port as key
        public static ConcurrentDictionary<int, int> PortToClientBuild { get; } = new ConcurrentDictionary<int, int>();

        public static async Task Init()
        {
            if (File.Exists(AccountsFile))
            {
                var json = await File.ReadAllTextAsync(AccountsFile);
                var accounts = JsonConvert.DeserializeObject<Dictionary<string, Account>>(json);
                Accounts = new ConcurrentDictionary<string, Account>(accounts);
            }
            else
            {
                Accounts = new ConcurrentDictionary<string, Account>();
            }
        }

        public static async Task Save()
        {
            var json = JsonConvert.SerializeObject(Accounts);
            await File.WriteAllTextAsync(AccountsFile, json);
        }

        public static Character GetCharacter(ulong charID) // TODO find a better way...
            => Sessions
                .SelectMany(x => x.Value.Account.Characters)
                .Where(c => c.ID == charID)
                .FirstOrDefault();
    }
}
