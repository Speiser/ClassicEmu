using System.Security.Cryptography;

namespace Classic.World.Cryptography.HeaderCrypts;

internal class WotLKHeaderCrypt : IHeaderCrypt
{
    private readonly static byte[] DecryptionKey = new byte[] { 0xC2, 0xB3, 0x72, 0x3C, 0xC6, 0xAE, 0xD9, 0xB5, 0x34, 0x3C, 0x53, 0xEE, 0x2F, 0x43, 0x67, 0xCE };
    private readonly static byte[] EncryptionKey = new byte[] { 0xCC, 0x98, 0xAE, 0x04, 0xE8, 0x97, 0xEA, 0xCA, 0x12, 0xDD, 0xC0, 0x93, 0x42, 0x91, 0x53, 0x57 };
    private readonly ARC4 encryptionService;
    private readonly ARC4 decryptionService;

    public WotLKHeaderCrypt(byte[] sessionKey)
    {
        using var decryptionSha = new HMACSHA1(DecryptionKey);
        var decryptionHash = decryptionSha.ComputeHash(sessionKey);
        this.decryptionService = new ARC4(decryptionHash, false);

        using var encryptionSha = new HMACSHA1(EncryptionKey);
        var encryptionHash = encryptionSha.ComputeHash(sessionKey);
        this.encryptionService = new ARC4(encryptionHash, true);
    }

    public byte[] Decrypt(byte[] data) => this.decryptionService.ProcessBytes(data);
    public byte[] Encrypt(byte[] data) => this.encryptionService.ProcessBytes(data);
}
