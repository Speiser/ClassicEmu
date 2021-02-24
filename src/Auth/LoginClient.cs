using System.Net.Sockets;
using System.Threading.Tasks;
using Classic.Auth.Challenges;
using Classic.Auth.Entities;
using Classic.Common;
using Classic.Cryptography;
using Classic.Data;
using Microsoft.Extensions.Logging;
using static Classic.Auth.Opcode;

namespace Classic.Auth
{
    public class LoginClient : ClientBase
    {
        private bool isReconnect;

        public LoginClient(ILogger<LoginClient> logger) : base(logger)
        {
        }

        public override async Task Initialize(TcpClient client)
        {
            await base.Initialize(client);

            this.logger.LogDebug($"{this.ClientInfo} - connected");

            await HandleConnection();
        }

        public SecureRemotePasswordProtocol SRP { get; internal set; }

        public GameVersion GameVersion { get; internal set; }

        protected override async Task HandlePacket(byte[] packet)
        {
            using var reader = new PacketReader(packet);
            var cmd = (Opcode)reader.ReadByte();
            this.LogAuthState($"Recv {cmd} ({packet.Length} bytes)");

            switch (cmd)
            {
                case LOGIN_CHALL:
                    await new ClientLogonChallenge(packet, this).Execute();
                    break;
                case LOGIN_PROOF:
                    var success = await new ClientLogonProof(packet, this).Execute();
                    if (success)
                    {
                        this.LogAuthState("Client authenticated");
                    }
                    else
                    {
                        this.isConnected = false;
                        this.LogAuthState("Client authentication failed");
                    }
                    break;
                case REALMLIST:
                    if (!this.isReconnect)
                    {
                        var session = new AccountSession(this.SRP.I, this.SRP.SessionKey);
                        DataStore.Sessions.TryAdd(this.SRP.I, session);
                    }
                    await ServerRealmlist.Send(this);
                    break;
                case RECONNECT_CHALLENGE:
                    this.isReconnect = true;
                    await new ClientReconnectChallenge(packet, this).Execute();
                    break;
                case RECONNECT_PROOF:
                    await new ClientReconnectProof(packet, this).Execute();
                    break;
            }
        }

        private void LogAuthState(string message) => this.logger.LogTrace($"{this.ClientInfo} - {message}");
    }
}