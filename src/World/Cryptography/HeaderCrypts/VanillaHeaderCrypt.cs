namespace Classic.World.Cryptography.HeaderCrypts
{
    // Based on
    // https://github.com/cmangos/mangos-classic/blob/master/src/shared/Auth/AuthCrypt.cpp
    internal class VanillaHeaderCrypt : IHeaderCrypt
    {
        private const int HeaderSize = 6;
        private readonly byte[] sessionKey;
        private int send_i = 0;
        private int send_j = 0;
        private int recv_i = 0;
        private int recv_j = 0;

        public VanillaHeaderCrypt(byte[] sessionKey)
        {
            this.sessionKey = sessionKey;
        }

        public byte[] Decrypt(byte[] data)
        {
            for (var i = 0; i < HeaderSize; i++)
            {
                this.recv_i %= (byte)this.sessionKey.Length;
                var x = (byte)((data[i] - this.recv_j) ^ this.sessionKey[this.recv_i]);
                this.recv_i++;
                this.recv_j = data[i];
                data[i] = x;
            }

            return data;
        }

        public byte[] Encrypt(byte[] data)
        {
            for (var i = 0; i < data.Length; i++)
            {
                this.send_i %= (byte)this.sessionKey.Length;
                var x = (byte)((data[i] ^ this.sessionKey[this.send_i]) + this.send_j);
                this.send_i++;
                data[i] = x;
                this.send_j = x;
            }

            return data;
        }
    }
}
