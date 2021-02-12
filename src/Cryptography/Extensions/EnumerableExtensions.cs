using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Classic.Cryptography.Extensions
{
    internal static class EnumerableExtensions
    {
        // https://github.com/miceiken/WoWClassicAuthServer/blob/master/WoWClassic.Common/Extensions.cs#L54
        public static BigInteger ToPositiveBigInteger(this IEnumerable<byte> bytes)
            => new BigInteger(bytes.Concat(new byte[] { 0 }).ToArray());
    }
}
