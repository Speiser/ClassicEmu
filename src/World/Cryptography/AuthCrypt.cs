namespace Classic.World.Cryptography
{
    // Based on
    // https://github.com/cmangos/mangos-classic/blob/master/src/shared/Auth/AuthCrypt.cpp
    public class AuthCrypt : IHeaderCrypt
    {
        private const int HeaderSize = 6;
        private readonly byte[] key;
        private int send_i = 0;
        private int send_j = 0;
        private int recv_i = 0;
        private int recv_j = 0;

        public AuthCrypt(byte[] key)
        {
            this.key = key;
        }

        public byte[] Decrypt(byte[] data)
        {
            for (var i = 0; i < HeaderSize; i++)
            {
                this.recv_i %= (byte)this.key.Length;
                var x = (byte)((data[i] - this.recv_j) ^ this.key[this.recv_i]);
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
                this.send_i %= (byte)this.key.Length;
                var x = (byte)((data[i] ^ this.key[this.send_i]) + this.send_j);
                this.send_i++;
                data[i] = x;
                this.send_j = x;
            }

            return data;
        }
    }
}
