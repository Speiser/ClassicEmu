using System.Net.Sockets;
using System.Threading.Tasks;
using Classic.Auth.Challenges;
using Classic.Common;
using Classic.Cryptography;
using Classic.Data;
using Microsoft.Extensions.Logging;

namespace Classic.Auth
{
    public class LoginClient : ClientBase
    {
        private ClientState state;
        public LoginClient(ILogger<LoginClient> logger) : base(logger)
        {
        }

        public override async Task Initialize(TcpClient client)
        {
            await base.Initialize(client);

            this.logger.LogDebug($"{this.ClientInfo} - connected");
            state = ClientState.Init;

            await HandleConnection();
        }

        public SecureRemotePasswordProtocol SRP { get; internal set; }

        protected override async Task HandlePacket(byte[] packet)
        {
            switch (this.state)
            {
                case ClientState.Init:
                    this.state = ClientState.LogonChallenge;
                    this.LogAuthState("Recv ClientLogonChallenge");
                    await new ClientLogonChallenge(packet, this).Execute();
                    this.LogAuthState("Sent ServerLogonChallenge");
                    break;
                case ClientState.LogonChallenge:
                    this.state = ClientState.LogonProof;
                    this.LogAuthState("Recv ClientLogonProof");
                    var success = await new ClientLogonProof(packet, this).Execute();
                    this.LogAuthState($"Sent ServerLogonProof {(success ? "successful" : "failed")}");
                    if (success)
                    {
                        this.state = ClientState.Authenticated;
                        this.LogAuthState("Client authenticated");
                    }
                    else
                    {
                        this.state = ClientState.Disconnected;
                        this.isConnected = false;
                        this.LogAuthState("Client authentication failed");
                    }
                    break;
                case ClientState.Authenticated:
                    DataStore.Users.TryAdd(this.SRP.I, new User(this.SRP));
                    this.LogAuthState("Sent Realmlist");
                    await ServerRealmList.Send(this);
                    break;
            }
        }

        private void LogAuthState(string message) => this.logger.LogTrace($"{this.ClientInfo} - {message}");
    }
}