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
            this.Send(new ServerAuthenticationChallenge().Get());
        }

        public AuthCrypt Crypt { get; }

        protected override void HandlePacket(byte[] packet)
        {
            this.LogClientPacket(packet);

            if (packet.Length == 14) return;

            var x = new ClientAuthenticationSession(packet);
            Console.WriteLine(x);

            this.Send(new ServerAuthenticationResponse(this.Crypt).Get(x));
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
