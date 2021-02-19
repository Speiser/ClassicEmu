using System.Linq;
using Classic.Common;
using Classic.Cryptography;

namespace Classic.World.HeaderUtil
{
    internal class VanillaHeaderUtil : IHeaderUtil
    {
        private readonly AuthCrypt crypt;

        public VanillaHeaderUtil(AuthCrypt crypt)
        {
            this.crypt = crypt;
        }

        // https://github.com/drolean/Servidor-Wow/blob/f77520bc8ad5d123139e34d3d0c8f40d161ad352/RealmServer/RealmServerSession.cs#L227
        public byte[] Encode(ServerMessageBase<Opcode> message)
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

            if (this.crypt.IsInitialized) header = this.crypt.Encrypt(header);

            return header.Concat(data).ToArray();
        }
    }
}
