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
        private const string CharactersFile = "chars.json";

        public static ConcurrentDictionary<string, Account> Accounts { get; private set; }

        public static ConcurrentDictionary<ulong, Character> Characters { get; private set; }

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

            if (File.Exists(CharactersFile))
            {
                var json = await File.ReadAllTextAsync(CharactersFile);
                var characters = JsonConvert.DeserializeObject<Dictionary<ulong, Character>>(json);
                Characters = new ConcurrentDictionary<ulong, Character>(characters);
            }
            else
            {
                Characters = new ConcurrentDictionary<ulong, Character>();
            }
        }

        public static async Task Save()
        {
            var accountJson = JsonConvert.SerializeObject(Accounts);
            await File.WriteAllTextAsync(AccountsFile, accountJson);

            var characterJson = JsonConvert.SerializeObject(Characters);
            await File.WriteAllTextAsync(CharactersFile, characterJson);
        }

        public static Character GetCharacter(ulong charID) => Characters.TryGetValue(charID, out var c) ? c : null;
    }
}
