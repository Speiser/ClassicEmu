using System;
using System.Net.Sockets;
using Classic.Common;
using Classic.Cryptography;
using Classic.Data;
using Classic.World.Messages;

namespace Classic.World
{
    public class WorldClient : ClientBase
    {
        public WorldClient(TcpClient client) : base(client)
        {
            this.Log("-- connected");
            this.Crypt = new AuthCrypt();
            this.Send(new SMSG_AUTH_CHALLENGE().Get());
        }

        public User User { get; internal set; }

        public AuthCrypt Crypt { get; }

        protected override void HandlePacket(byte[] data)
        {
            for (var i = 0; i < data.Length; i++)
            {
                // TODO: Spans instead of array.copy!
                var header = new byte[6];
                Array.Copy(data, i, header, 0, 6);

                var (length, opcode) = this.DecodePacket(header);

                this.Log($"{opcode} - {length} bytes");

                var packet = new byte[length];
                Array.Copy(data, i + 6, packet, 0, length - 4);

                var handler = WorldPacketHandler.GetHandler(opcode);
                handler(this, packet);

                i += 2 + (length - 1);
            }
        }

        public void SendPacket(ServerMessageBase<Opcode> message)
        {
            var data = this.Crypt.Encode(message);
            this.Send(data);
            message.Dispose();
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
    }
}
