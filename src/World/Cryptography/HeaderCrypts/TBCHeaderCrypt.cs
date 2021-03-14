using System.Security.Cryptography;

namespace Classic.World.Cryptography.HeaderCrypts
{
    internal class TBCHeaderCrypt : IHeaderCrypt
    {
        private readonly static byte[] ShaKey = new byte[] { 0x38, 0xA7, 0x83, 0x15, 0xF8, 0x92, 0x25, 0x30, 0x71, 0x98, 0x67, 0xB1, 0x8C, 0x4, 0xE2, 0xAA };
        private readonly VanillaHeaderCrypt vanillaHeaderCrypt;

        public TBCHeaderCrypt(byte[] sessionKey)
        {
            using var hash = new HMACSHA1(ShaKey);
            var vanillaKey = hash.ComputeHash(sessionKey);
            this.vanillaHeaderCrypt = new(hash.ComputeHash(sessionKey));
        }

        public byte[] Decrypt(byte[] data) => this.vanillaHeaderCrypt.Decrypt(data);
        public byte[] Encrypt(byte[] data) => this.vanillaHeaderCrypt.Encrypt(data);
    }
}
