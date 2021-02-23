﻿using System.IO;
using System.Linq;
using Classic.Common;
using Classic.Cryptography;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace Classic.World.HeaderUtil
{
    internal class VanillaTBCHeaderUtil : IHeaderUtil
    {
        private readonly AuthCrypt crypt;

        public VanillaTBCHeaderUtil(AuthCrypt crypt)
        {
            this.crypt = crypt;
        }

        // https://github.com/drolean/Servidor-Wow/blob/f77520bc8ad5d123139e34d3d0c8f40d161ad352/RealmServer/RealmServerSession.cs#L227
        public byte[] Encode(ServerMessageBase<Opcode> message)
        {
            var data = message.Get();
            var index = 0;
            var header = new byte[4];

            if (message.Opcode == Opcode.SMSG_UPDATE_OBJECT && data.Length > 98)
            {
                var uncompressed = data.Length;
                message.Opcode = Opcode.SMSG_COMPRESSED_UPDATE_OBJECT;
                data = Compress(data);
                data = new PacketWriter().WriteUInt32((uint)uncompressed).WriteBytes(data).Build();
            }

            var newSize = data.Length + 2;

            //if (newSize > 0x7FFF)
            //    header[index++] = (byte)(0x80 | (0xFF & (newSize >> 16)));

            header[index++] = (byte)(0xFF & (newSize >> 8));
            header[index++] = (byte)(0xFF & newSize);
            header[index++] = (byte)(0xFF & (int)message.Opcode);
            header[index] = (byte)(0xFF & ((int)message.Opcode >> 8));

            if (this.crypt.IsInitialized) header = this.crypt.Encrypt(header);

            return header.Concat(data).ToArray();
        }

        // TODO Extract
        private static byte[] Compress(byte[] data)
        {
            using var outputStream = new MemoryStream();
            using var compressordStream = new DeflaterOutputStream(outputStream);
            compressordStream.Write(data, 0, data.Length);
            compressordStream.Flush();
            return outputStream.ToArray();
        }
    }
}
