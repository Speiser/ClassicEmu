using System.Security.Cryptography;

namespace Classic.Shared.Cryptography
{
    public static class Random
    {
        private static readonly RandomNumberGenerator rng = new RNGCryptoServiceProvider();
        private static readonly System.Random rand = new System.Random();

        public static byte[] GetBytes(int size)
        {
            var bytes = new byte[size];
            rng.GetNonZeroBytes(bytes);
            return bytes;
        }

        public static ulong GetUInt64()
        {
            var thirtyBits = (uint)rand.Next(1 << 30);
            var twoBits = (uint)rand.Next(1 << 2);

            return (thirtyBits << 2) | twoBits;
        }
    }
}