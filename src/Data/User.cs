using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using Classic.Cryptography;

namespace Classic.Data
{
    public class User
    {
        private readonly SecureRemotePasswordProtocol srp;
        public User(SecureRemotePasswordProtocol srp)
        {
            this.srp = srp;
            this.Characters = new ConcurrentBag<Character>();
        }

        public string Identifier => this.srp.I;
        public byte[] SessionKey => this.srp.SessionKey;
        public ConcurrentBag<Character> Characters { get; }
    }
}
