using System.Net.Sockets;
using System.Threading.Tasks;
using Classic.Auth.Challenges;
using Classic.Auth.Entities;
using Classic.Shared;
using Classic.Auth.Cryptography;
using Microsoft.Extensions.Logging;
using static Classic.Auth.Opcode;
using Classic.Shared.Data;

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
                        var account = AccountStore.GetAccount(this.SRP.I);

                        // for development, create new account if not found
                        if (account is null)
                        {
                            account = new Account { Identifier = this.SRP.I };
                            AccountStore.AddAccount(account);
                        }

                        var session = new AccountSession(account, this.SRP.SessionKey);
                        AccountStore.AddSession(session);
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