using System.Linq;
using Classic.Common;

namespace Classic.Cryptography
{
    // Based on
    // https://github.com/cmangos/mangos-classic/blob/master/src/shared/Auth/AuthCrypt.cpp
    public class AuthCrypt
    {
        private byte[] key;
        private int send_i = 0;
        private int send_j = 0;
        private int recv_i = 0;
        private int recv_j = 0;

        public bool IsInitialized { get; private set; }

        public byte[] Decrypt(byte[] data, int size)
        {
            for (int i = 0; i < size; i++)
            {
                this.recv_i %= (byte)this.key.Length;
                var x = (byte)((data[i] - this.recv_j) ^ this.key[this.recv_i]);
                this.recv_i++;
                this.recv_j = data[i];
                data[i] = x;
            }

            return data;
        }

        // https://github.com/drolean/Servidor-Wow/blob/master/Common/Helpers/Utils.cs#L13
        public byte[] Encode(ServerMessageBase<World.Opcode> message)
        {
            var data = message.Get();
            var index = 0;
            var newSize = data.Length + 2;
            var header = new byte[4];

            if (newSize > 0x7FFF)
                header[index++] = (byte)(0x80 | (0xFF & (newSize >> 16)));

            header[index++] = (byte)(0xFF & (newSize >> 8));
            header[index++] = (byte)(0xFF & (newSize >> 0));
            header[index++] = (byte)(0xFF & (int)message.Opcode);
            header[index] = (byte)(0xFF & ((int)message.Opcode >> 8));

            if (this.IsInitialized) header = this.Encrypt(header);

            return header.Concat(data).ToArray();
        }

        private byte[] Encrypt(byte[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                this.send_i %= (byte)this.key.Length;
                var x = (byte)((data[i] ^ this.key[this.send_i]) + this.send_j);
                this.send_i++;
                data[i] = x;
                this.send_j = x;
            }

            return data;
        }

        public void SetKey(byte[] key)
        {
            this.key = key;
            this.IsInitialized = true;
        }
    }
}
