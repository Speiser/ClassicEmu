using System.Collections.Concurrent;
using System.IO;
using Classic.Auth.Entities;
using Classic.Cryptography;
using Newtonsoft.Json;

namespace Classic.Data
{
    public class User
    {
        public const string CharsFile = "chars.json";
        private readonly SecureRemotePasswordProtocol srp;

        public User(SecureRemotePasswordProtocol srp, GameVersion gameVersion)
        {
            this.srp = srp;
            this.GameVersion = gameVersion;

            // TODO
            if (File.Exists(CharsFile))
            {
                var raw = File.ReadAllText(CharsFile);
                var chars = JsonConvert.DeserializeObject<Character[]>(raw);

                this.Characters = new ConcurrentBag<Character>(chars);
            }
            else
            {
                this.Characters = new ConcurrentBag<Character>();
            }
        }

        public string Identifier => this.srp.I;
        public byte[] SessionKey => this.srp.SessionKey;
        public ConcurrentBag<Character> Characters { get; }
        public GameVersion GameVersion { get; }
    }
}
