using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;

namespace Classic.World.Cryptography;

internal class ARC4
{
    private readonly RC4Engine rc4;

    public ARC4(byte[] sessionKey, bool forEncryption)
    {
        this.rc4 = new RC4Engine();
        this.rc4.Init(forEncryption, new KeyParameter(sessionKey));

        // Drop first 1024 bytes
        this.ProcessBytes(new byte[1024]);
    }

    public byte[] ProcessBytes(byte[] data)
    {
        this.rc4.ProcessBytes(data, 0, data.Length, data, 0);
        return data;
    }
}
