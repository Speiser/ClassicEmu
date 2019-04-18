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

        protected override void HandlePacket(byte[] packet)
        {
            var opcode = this.LogPacket(packet);

            var handler = WorldPacketHandler.GetHandler(opcode);

            handler(this, packet);
        }

        public void SendPacket(ServerMessageBase<Opcode> message)
        {
            var data = this.Crypt.Encode(message);
            this.Send(data);
            message.Dispose();
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

        private new Opcode LogPacket(byte[] packet)
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
