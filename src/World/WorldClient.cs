using Classic.Common;
using Classic.World.Authentication;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Classic.Cryptography;

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

        public AuthCrypt Crypt { get; }

        protected override void HandlePacket(byte[] packet)
        {
            this.LogClientPacket(packet);

            var (length, opcode) = DecodePacket(packet);

            this.Log($"{opcode} - {length} bytes");

            if (packet.Length == 14) return;

            var x = new ClientAuthenticationSession(packet);
            Console.WriteLine(x);

            this.Send(new ServerAuthenticationResponse(this.Crypt).Get(x));
        }

        public void SendPacket(WorldPacket packet)
        {
        }

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
