using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Classic.World.Cryptography
{
    // TODO: Move arc4 out and rename to WotlkHeaderCrypt?
    public class ARC4 : IHeaderCrypt
    {
        private readonly static byte[] DecryptionKey = new byte[] { 0xC2, 0xB3, 0x72, 0x3C, 0xC6, 0xAE, 0xD9, 0xB5, 0x34, 0x3C, 0x53, 0xEE, 0x2F, 0x43, 0x67, 0xCE };
        private readonly static byte[] EncryptionKey = new byte[] { 0xCC, 0x98, 0xAE, 0x04, 0xE8, 0x97, 0xEA, 0xCA, 0x12, 0xDD, 0xC0, 0x93, 0x42, 0x91, 0x53, 0x57 };

        private readonly byte[] decryptionHash;
        private readonly byte[] encryptionHash;

        public ARC4(byte[] sessionKey)
        {
            using var decryptionSha = new HMACSHA1(DecryptionKey);
            this.decryptionHash = decryptionSha.ComputeHash(sessionKey);
            using var encryptionSha = new HMACSHA1(EncryptionKey);
            this.encryptionHash = encryptionSha.ComputeHash(sessionKey);
        }

        public byte[] Decrypt(byte[] data) => throw new NotImplementedException();
        public byte[] Encrypt(byte[] data) => throw new NotImplementedException();
    }
}
