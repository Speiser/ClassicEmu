using System;
using System.Net.Sockets;
using Classic.Common;
using Classic.Cryptography;
using Classic.Data;
using Classic.World.Authentication;
using Classic.World.Character;
using Classic.World.Player;
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

        public User User { get; private set; }

        public AuthCrypt Crypt { get; }

        protected override void HandlePacket(byte[] packet)
        {
            var opcode = this.LogPacket(packet);

            // TODO: replace with Handler dict?
            // TODO: NAMING CONVENTION FOR PACKETS
            switch (opcode)
            {
                case CMSG_AUTH_SESSION:
                    if (this.authenticated) throw new InvalidOperationException($"{nameof(CMSG_AUTH_SESSION)} Already authed");
                    this.authenticated = true;
                    var receivedClientProof = new ClientAuthenticationSession(packet);
                    this.SendPacket(
                        new ServerAuthenticationResponse(this.Crypt).Get(receivedClientProof),
                        SMSG_AUTH_RESPONSE);
                    DataStore.Users.TryGetValue(receivedClientProof.account_name, out var user);
                    this.User = user;
                    break;
                case CMSG_CHAR_ENUM:
                    this.SendPacket(new CharacterEnum().GetCharacters(this.User), SMSG_CHAR_ENUM);
                    break;
                case CMSG_CHAR_CREATE:
                    var character = new CharacterCreateRequest().Read(packet);
                    this.User.Characters.Add(character);
                    this.SendPacket(new CharacterCreateResponse().Get(), SMSG_CHAR_CREATE);
                    break;
                case CMSG_PLAYER_LOGIN:
                    // TODO Store playerhandler instance somewhere on worldserver
                    new PlayerHandler(packet, this);
                    break;
                default:
                    this.Log($"UNHANDLED CMD {opcode}");
                    break;
            }
        }

        private void SendPacket(byte[] data, Opcode opcode)
        {
            var header = this.Encode(data.Length, (int)opcode);
            this.Send(new WorldPacket(header, data).ToByteArray());
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
