using Classic.Common;
using Classic.World.Authentication;
using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using Classic.Cryptography;
using static Classic.World.Opcode;

namespace Classic.World
{
    public class WorldClient : ClientBase
    {
        private bool authenticated;

        public WorldClient(TcpClient client) : base(client)
        {
            this.Log("-- connected");
            this.Crypt = new AuthCrypt();
            this.Send(ServerAuthenticationChallenge.Create());
        }

        public AuthCrypt Crypt { get; }

        protected override void HandlePacket(byte[] packet)
        {
            if (!this.authenticated)
            {
                this.HandleAuthentication(packet);
                return;
            }

            this.LogClientPacket(packet);

            var (length, opcode) = DecodePacket(packet);

            this.Log($"{opcode} - {length} bytes");

            if (packet.Length == 14) return;
        }

        private void HandleAuthentication(byte[] packet)
        {
            var receivedClientProof = new ClientAuthenticationSession(packet);
            this.SendPacket(
                new ServerAuthenticationResponse(this.Crypt).Get(receivedClientProof),
                SMSG_AUTH_RESPONSE);
            this.authenticated = true;
        }

        public void SendPacket(byte[] data, Opcode opcode)
        {
            var header = this.Encode(data.Length, (int)opcode);

            this.SendPacket(new WorldPacket(header, data));
        }

        public void SendPacket(WorldPacket packet)
        {
            this.Send(packet.ToByteArray());
        }

        // https://github.com/drolean/Servidor-Wow/blob/master/Common/Helpers/Utils.cs#L13
        public byte[] Encode(int size, int opcode)
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

        private void LogClientPacket(byte[] packet)
        {
            using (var packetStream = new MemoryStream(packet))
            {
                using (var packetReader = new BinaryReader(packetStream))
                {
                    // TODO: I can probably extract the read len + cmd part.
                    var lengthBytes = packetReader.ReadBytes(2);
                    Array.Reverse(lengthBytes); // len is bigendian

                    var len = (ushort)BitConverter.ToInt16(lengthBytes);

                    var cmd = packetReader.ReadUInt16();

                    this.Log($"Packet received - Len {len} - Cmd {cmd}");
                    this.Log(Encoding.ASCII.GetString(packet));
                }
            }
        }
    }
}
