using System.IO;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace Classic.World.Cryptography;

public class Compression
{
    public static byte[] Compress(byte[] data)
    {
        using var outputStream = new MemoryStream();
        using var compressordStream = new DeflaterOutputStream(outputStream);
        compressordStream.Write(data, 0, data.Length);
        compressordStream.Flush();
        return outputStream.ToArray();
    }

    public static byte[] Uncompress(byte[] data)
    {
        using var outputStream = new MemoryStream();
        using var compressedStream = new MemoryStream(data);
        using var inputStream = new InflaterInputStream(compressedStream);
        inputStream.CopyTo(outputStream);
        outputStream.Position = 0;
        return outputStream.ToArray();
    }
}
