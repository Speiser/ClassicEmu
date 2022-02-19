using System;

namespace Classic.World.Extensions;

internal static class ByteExtensions
{
    public static T AsEnum<T>(this byte b) => (T)Enum.ToObject(typeof(T), b);
}
