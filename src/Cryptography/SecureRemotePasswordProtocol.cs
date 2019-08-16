using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using Classic.Common;

namespace Classic.Cryptography
{
    public class SecureRemotePasswordProtocol
    {
        public const int g = 7;

        /// <summary>
        /// The salt, a "random" value.
        /// </summary>
        public readonly byte[] s;

        private readonly SHA1 sha = new SHA1CryptoServiceProvider();

        /// <summary>
        /// The server's secret value.
        /// </summary>
        private readonly BigInteger b;

        /// <summary>
        /// The SRP6 Verification value.
        /// </summary>
        private readonly BigInteger v;

        public SecureRemotePasswordProtocol(string username, string password)
        {
            this.N = BigInteger.Parse("62100066509156017342069496140902949863249758336000796928566441170293728648119");
            this.I = username;
            this.b = this.GetRandom(19);
            this.s = this.GetRandom(32).ToProperByteArray();

            // The combined hash of salt + Identifier:Password.
            // SHA1(s | SHA1(I | ":" | P))
            var x = this.GetHashedCredentials(password);

            this.v = BigInteger.ModPow(g, x, this.N);

            var gmod = BigInteger.ModPow(g, this.b, this.N);
            var tempB = (3 * this.v + gmod) % this.N;

            this.B = tempB.ToProperByteArray();
        }

        /// <summary>
        /// Gets the client's public value.
        /// </summary>
        public byte[] A { get; private set; }

        /// <summary>
        /// Gets the server's public value.
        /// </summary>
        public byte[] B { get; }

        public byte[] SessionKey { get; private set; }

        /// <summary>
        /// The hashed session key, hashed with SHA1.
        /// </summary>
        public byte[] K { get; private set; }

        /// <summary>
        /// M = H(H(N) xor H(g), H(I), s, A, B, K)
        /// </summary>
        public byte[] M { get; private set; }

        /// <summary>
        /// Gets a safe-prime.
        /// Hardcoded to: 0x894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7
        /// </summary>
        public BigInteger N { get; }

        /// <summary>
        /// Gets the identifier (username).
        /// </summary>
        public string I { get; }

        public bool ValidateClientProof(byte[] clientPublicValue, byte[] clientProof)
        {
            this.A = clientPublicValue;

            // u = H(A, B)
            // u is the so called "Random scrambling parameter".
            var u = this.sha.ComputeHash(this.A.Concat(this.B).ToArray()).ToPositiveBigInteger();

            var sessionKeyAsByte = this.CalculateSessionKey(u).ToProperByteArray();
            this.K = this.sha.ComputeHash(sessionKeyAsByte);
            
            var vK = new int[40];
            var t1 = new List<byte>();

            for (int i = 0; i < 16; i++)
                t1.Add(sessionKeyAsByte[i * 2]);

            byte[] t1_hash = this.sha.ComputeHash(t1.ToArray());

            for (int i = 0; i < 20; i++)
                vK[i * 2] = t1_hash[i];

            for (int i = 0; i < 16; i++)
                t1[i] = sessionKeyAsByte[i * 2 + 1];

            t1_hash = this.sha.ComputeHash(t1.ToArray());

            for (int i = 0; i < 20; i++)
                vK[i * 2 + 1] = t1_hash[i];

            this.SessionKey = Array.ConvertAll(vK, Convert.ToByte);

            var safePrimeHash = this.sha.ComputeHash(this.N.ToByteArray().Take(32).ToArray());
            var gHash = this.sha.ComputeHash(new BigInteger(g).ToByteArray());

            var t3 = new List<int>();
            for (int i = 0; i < 20; i++)
                t3.Add(safePrimeHash[i] ^ gHash[i]);

            var t4 = this.sha.ComputeHash(Encoding.ASCII.GetBytes(this.I));

            var t3Bytes = Array.ConvertAll(t3.ToArray(), Convert.ToByte);
            var computedClientProof =
                this.sha.ComputeHash(
                    // t3 + t4 + salt + A + B + sessionKey
                    t3Bytes.Concat(t4).Concat(this.s).Concat(this.A).Concat(this.B).Concat(this.SessionKey).ToArray());

            if (!computedClientProof.SequenceEqual(clientProof))
                return false;

            this.M = this.sha.ComputeHash(this.A.Concat(clientProof).Concat(this.SessionKey).ToArray());
            return true;
        }

        private BigInteger CalculateSessionKey(BigInteger u)
        {
            // S = (A * (v^u) % N) ^ b % N
            var intA = new BigInteger(this.A);

            // (v^u) % N
            var innerModPow = BigInteger.ModPow(this.v, u, this.N);
            return BigInteger.ModPow(intA * innerModPow, this.b, this.N);
        }

        private BigInteger GetRandom(int length)
            => Random.GetBytes(length).ToPositiveBigInteger() % this.N;

        private BigInteger GetHashedCredentials(string password)
        {
            // TODO: At some point ill use the correct password
            var temp = this.sha.ComputeHash(Encoding.ASCII.GetBytes($"{this.I}:{this.I}".ToUpper()));
            var hash = this.sha.ComputeHash(this.s.Concat(temp).ToArray());
            return hash.ToPositiveBigInteger();
        }
    }
}
