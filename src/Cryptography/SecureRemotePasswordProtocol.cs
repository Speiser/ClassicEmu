using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace Classic.Cryptography
{
    public class SecureRemotePasswordProtocol
    {
        public const int g = 7;
        
        /// <summary>
        /// The salt, a "random" value.
        /// </summary>
        public readonly byte[] s = new byte[] {
            0xF4, 0x3C, 0xAA, 0x7B, 0x24, 0x39, 0x81, 0x44,
            0xBF, 0xA5, 0xB5, 0x0C, 0x0E, 0x07, 0x8C, 0x41,
            0x03, 0x04, 0x5B, 0x6E, 0x57, 0x5F, 0x37, 0x87,
            0x31, 0x9F, 0xC4, 0xF8, 0x0D, 0x35, 0x94, 0x29
        };

        private readonly SHA1 sha = new SHA1CryptoServiceProvider();

        /// <summary>
        /// The server's secret value.
        /// </summary>
        private readonly BigInteger b;

        /// <summary>
        /// The SRP6 Verification value.
        /// </summary>
        private readonly BigInteger v;

        /// <summary>
        /// The combined hash of salt + Identifier:Password.
        /// SHA1(s | SHA1(I | ":" | P))
        /// </summary>
        private readonly byte[] x;

        /// <summary>
        /// The so called "Random scrambling parameter".
        /// </summary>
        private byte[] u;

        public SecureRemotePasswordProtocol(string username, string password)
        {
            // TODO: Verify that N is a "good" value
            this.N = BigInteger.Parse("62100066509156017342069496140902949863249758336000796928566441170293728648119");
            this.I = username;
            this.P = password;
            this.b = this.Generateb();
            this.x = this.Calculatex();

            var intx = new BigInteger(this.x);

            if (intx < 0)
                throw new ArgumentException("this.x was negative.");

            this.v = BigInteger.ModPow(g, intx, this.N);

            var gmod = BigInteger.ModPow(g, this.b, this.N);
            this.B = (((3 * this.v) + gmod) % this.N).ToByteArray();
        }

        /// <summary>
        /// Gets the client's public value.
        /// </summary>
        public byte[] A { get; private set; }

        /// <summary>
        /// Gets the server's public value.
        /// </summary>
        public byte[] B { get; }

        /// <summary>
        /// Gets the session key.
        /// </summary>
        public BigInteger S { get; private set; }

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

        /// <summary>
        /// Gets the cleartext password.
        /// </summary>
        public string P { get; }

        public bool ValidateClientProof(byte[] clientPublicValue, byte[] clientProof)
        {
            this.A = clientPublicValue;

            // u = H(A, B)
            this.u = this.sha.ComputeHash(this.A.Concat(this.B).ToArray());
            this.S = this.CalculateSessionKey();

            var sessionKeyAsByte = this.S.ToByteArray();
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

            var sessionKey = Array.ConvertAll(vK, Convert.ToByte);

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
                    t3Bytes.Concat(t4).Concat(this.s).Concat(this.A).Concat(this.B).Concat(sessionKey).ToArray());

            if (!computedClientProof.SequenceEqual(clientProof))
                return false;

            this.M = this.sha.ComputeHash(this.A.Concat(clientProof).Concat(sessionKey).ToArray());
            return true;
        }

        private BigInteger CalculateSessionKey()
        {
            // S = (A * (v^u) % N) ^ b % N
            var intA = new BigInteger(this.A);
            var intu = new BigInteger(this.u);

            // (v^u) % N
            var innerModPow = BigInteger.ModPow(this.v, BigInteger.Abs(intu), this.N);
            return BigInteger.ModPow((intA * innerModPow), this.b, this.N);
        }

        private BigInteger Generateb()
        {
            var random = new BigInteger(Random.GetBytes(152));
            return BigInteger.Abs(random) % this.N;
        }

        private byte[] Calculatex()
        {
            var temp = this.sha.ComputeHash(Encoding.ASCII.GetBytes($"{this.I}:{this.P}"));
            return this.sha.ComputeHash(this.s.Concat(temp).ToArray());
        }
    }
}
