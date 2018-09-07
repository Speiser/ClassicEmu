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

            //if (bytes[0] == 0)
            //    bytes[0] = 1; // only allow positive values

            return bytes;
        }
    }
}