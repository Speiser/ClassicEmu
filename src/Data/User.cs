using System.Collections.Concurrent;
using System.IO;
using Classic.Cryptography;
using Newtonsoft.Json;

namespace Classic.Data
{
    public class User
    {
        private readonly SecureRemotePasswordProtocol srp;
        public User(SecureRemotePasswordProtocol srp)
        {
            this.srp = srp;

            // TODO
            var raw = File.ReadAllText("chars.json");
            var chars = JsonConvert.DeserializeObject<Character[]>(raw);

            this.Characters = new ConcurrentBag<Character>(chars);
        }

        public string Identifier => this.srp.I;
        public byte[] SessionKey => this.srp.SessionKey;
        public ConcurrentBag<Character> Characters { get; }
    }
}
