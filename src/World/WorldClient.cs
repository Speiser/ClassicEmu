using System;
using System.Linq;
using System.Net.Sockets;
using Classic.Common;
using Classic.Cryptography;
using Classic.Data;
using Classic.World.Authentication;

namespace Classic.World
{
    public class WorldClient : ClientBase
    {
        public WorldClient(TcpClient client) : base(client)
        {
            this.Log("-- connected");
            this.Crypt = new AuthCrypt();
            this.Send(ServerAuthenticationChallenge.Create());
        }

        public User User { get; internal set; }

        public AuthCrypt Crypt { get; }

        protected override void HandlePacket(byte[] packet)
        {
            var opcode = this.LogPacket(packet);

            var handler = WorldPacketHandler.GetHandler(opcode);

            handler(this, packet);
        }

        public void SendPacket(byte[] data, Opcode opcode)
        {
            var header = this.Encode(data.Length, (int)opcode);
            this.Send(header.Concat(data).ToArray());
        }

        // https://github.com/drolean/Servidor-Wow/blob/master/Common/Helpers/Utils.cs#L13
        private byte[] Encode(int size, int opcode)
        {
            int index = 0;
            int newSize = size + 2;
            byte[] header = new byte[4];
            if (newSize > 0x7FFF)
                header[index++] = (byte)(0x80 | (0xFF & (newSize >> 16)));

            header[index++] = (byte)(0xFF & (newSize >> 8));
            header[index++] = (byte)(0xFF & (newSize >> 0));
            header[index++] = (byte)(0xFF & opcode);
            header[index] = (byte)(0xFF & (opcode >> 8));

            if (this.Crypt.IsInitialized) header = this.Crypt.Encrypt(header);

            return header;
        }

        // Based on
        // https://github.com/drolean/Servidor-Wow/blob/master/Common/Helpers/Utils.cs#L31
        private (ushort length, Opcode opcode) DecodePacket(byte[] packet)
        {
            ushort length;
            short opcode;

            if (this.Crypt.IsInitialized)
            {
                this.Crypt.Decrypt(packet, 6);
                length = BitConverter.ToUInt16(new [] { packet[1], packet[0] });
                opcode = BitConverter.ToInt16(new[] { packet[2], packet[3] });
            }
            else
            {
                length = BitConverter.ToUInt16(new[] { packet[1], packet[0] });
                opcode = BitConverter.ToInt16(packet, 2);
            }

            return (length, (Opcode)opcode);
        }

        private Opcode LogPacket(byte[] packet)
        {
            // Copy so that the original packet is not corrupted
            var copy = new byte[packet.Length];
            Array.Copy(packet, copy, copy.Length);
            var (length, opcode) = this.DecodePacket(copy);
            this.Log($"{opcode} - {length} bytes");
            return opcode;
        }
    }
}
