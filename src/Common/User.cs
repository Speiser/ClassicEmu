using System.Numerics;
using Classic.Cryptography;

namespace Classic.Common
{
    public class User
    {
        private readonly SecureRemotePasswordProtocol srp;
        public User(SecureRemotePasswordProtocol srp)
        {
            this.srp = srp;
        }

        public string Identifier => this.srp.I;
        public BigInteger SessionKey => this.srp.S;
    }
}
