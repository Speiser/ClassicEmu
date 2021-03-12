using System.Net.Sockets;
using System.Threading.Tasks;
using Classic.Auth.Challenges;
using Classic.Auth.Cryptography;
using Classic.Shared;
using Classic.Shared.Data;
using Classic.Shared.Services;
using Microsoft.Extensions.Logging;

namespace Classic.Auth
{
    public class LoginClient : ClientBase
    {
        private bool isReconnect;

        public LoginClient(ILogger<LoginClient> logger, AccountService accountService) : base(logger)
        {
            this.AccountService = accountService;
        }

        public AccountService AccountService { get; }

        public override async Task Initialize(TcpClient client)
        {
            await base.Initialize(client);

            this.logger.LogDebug($"{this.ClientInfo} - connected");

            await HandleConnection();
        }

        public SecureRemotePasswordProtocol SRP { get; internal set; }

        public int Build { get; internal set; }

        protected override async Task HandlePacket(byte[] packet)
        {
            using var reader = new PacketReader(packet);
            var cmd = (Opcode)reader.ReadByte();
            this.LogAuthState($"Recv {cmd} ({packet.Length} bytes)");

            switch (cmd)
            {
                case Opcode.LoginChallenge:
                    await new ClientLogonChallenge(packet, this).Execute();
                    break;
                case Opcode.LoginProof:
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
                case Opcode.Realmlist:
                    if (!this.isReconnect)
                    {
                        var account = this.AccountService.GetAccount(this.SRP.I);

                        // for development, create new account if not found
                        if (account is null)
                        {
                            account = new Account { Identifier = this.SRP.I };
                            this.AccountService.AddAccount(account);
                        }

                        var session = new AccountSession(this.SRP.I, this.SRP.SessionKey);
                        this.AccountService.AddSession(session);
                    }
                    await ServerRealmlist.Send(this);
                    break;
                case Opcode.ReconnectChallenge:
                    this.isReconnect = true;
                    await new ClientReconnectChallenge(packet, this).Execute();
                    break;
                case Opcode.ReconnectProof:
                    await new ClientReconnectProof(packet, this).Execute();
                    break;
            }
        }

        private void LogAuthState(string message) => this.logger.LogTrace($"{this.ClientInfo} - {message}");
    }
}