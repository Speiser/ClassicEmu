using System.Security.Cryptography;

namespace Classic.Cryptography
{
    public static class Random
    {
        private static readonly RandomNumberGenerator rng = new RNGCryptoServiceProvider();

        public static byte[] GetBytes(int size)
        {
            var bytes = new byte[size];
            rng.GetNonZeroBytes(bytes);
            return bytes;
        }
    }
}