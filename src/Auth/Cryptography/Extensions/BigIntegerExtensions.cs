using System;
using System.Numerics;

namespace Classic.Auth.Cryptography.Extensions;

public static class BigIntegerExtensions
{
    // https://github.com/miceiken/WoWClassicAuthServer/blob/master/WoWClassic.Common/Extensions.cs#L45
    public static byte[] ToProperByteArray(this BigInteger b)
    {
        var bytes = b.ToByteArray();
        if (b.Sign == 1 && bytes.Length > 1 && bytes[bytes.Length - 1] == 0)
            Array.Resize(ref bytes, bytes.Length - 1);
        return bytes;
    }
}
