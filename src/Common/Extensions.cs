using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Classic.Common
{
    public static class Extensions
    {
        // https://github.com/miceiken/WoWClassicAuthServer/blob/master/WoWClassic.Common/Extensions.cs#L45
        public static byte[] ToProperByteArray(this BigInteger b)
        {
            var bytes = b.ToByteArray();
            if (b.Sign == 1 && bytes.Length > 1 && bytes[bytes.Length - 1] == 0)
                Array.Resize(ref bytes, bytes.Length - 1);
            return bytes;
        }

        // https://github.com/miceiken/WoWClassicAuthServer/blob/master/WoWClassic.Common/Extensions.cs#L54
        public static BigInteger ToPositiveBigInteger(this IEnumerable<byte> bytes)
            => new BigInteger(bytes.Concat(new byte[] { 0 }).ToArray());
    }
}
