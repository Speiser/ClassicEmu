using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Classic.Common;
using Classic.Cryptography;
using Classic.Data;
using Classic.World.Entities;
using Classic.World.Extensions;
using Classic.World.Messages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Classic.World
{
    public class WorldClient : ClientBase
    {
        private readonly WorldPacketHandler packetHandler;

        public WorldClient(WorldPacketHandler packetHandler, ILogger<WorldClient> logger) : base(logger)
        {
            this.packetHandler = packetHandler;
        }

        public override async Task Initialize(TcpClient client)
        {
            await base.Initialize(client);

            Log("-- connected");
            Crypt = new AuthCrypt(); // TODO DI??

            await Send(new SMSG_AUTH_CHALLENGE().Get());
            await HandleConnection();
        }

        public User User { get; internal set; }
        public PlayerEntity Player { get; internal set; }
        public Character Character => Player?.Character;

        public AuthCrypt Crypt { get; private set; }

        protected override async Task HandlePacket(byte[] data)
        {
            for (var i = 0; i < data.Length; i++)
            {
                // TODO: Spans instead of array.copy!
                var header = new byte[6];
                Array.Copy(data, i, header, 0, 6);

                var (length, opcode) = this.DecodePacket(header);

                logger.LogOpcode(opcode, length);

                var packet = new byte[length];
                Array.Copy(data, i + 6, packet, 0, length - 4);

                var handler = packetHandler.GetHandler(opcode);
                await handler(this, packet);

                i += 2 + (length - 1);
            }
        }

        public async Task SendPacket(ServerMessageBase<Opcode> message)
        {
            var data = this.Crypt.Encode(message);
            await this.Send(data);
            message.Dispose();
        }

        protected override void OnDisconnected()
        {
            // TODO
            var json = JsonConvert.SerializeObject(User.Characters.ToArray());
            File.WriteAllText("chars.json", json);
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
