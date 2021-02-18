using System.Net.Sockets;
using System.Threading.Tasks;
using Classic.Auth.Challenges;
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
            this.logger.LogWarning($"Received {packet.Length} bytes. Cmd: {cmd}");

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
                        DataStore.Users.TryAdd(this.SRP.I, new User(this.SRP));
                    }
                    await ServerRealmList.Send(this);
                    break;
                case RECONNECT_CHALLENGE:
                    this.isReconnect = true;
                    await new ClientReconnectChallenge(packet, this).Execute();
                    break;
                case RECONNECT_PROOF:
                    await new ClientReconnectProof(packet, this).Execute();
                    break;
            }

            //switch (this.state)
            //{
            //    case ClientState.Init:
            //        this.state = ClientState.LogonChallenge;
            //        this.LogAuthState("Recv ClientLogonChallenge");
            //        await new ClientLogonChallenge(packet, this).Execute();
            //        this.LogAuthState("Sent ServerLogonChallenge");
            //        break;
            //    case ClientState.LogonChallenge:
            //        this.state = ClientState.LogonProof;
            //        this.LogAuthState("Recv ClientLogonProof");
            //        var success = await new ClientLogonProof(packet, this).Execute();
            //        this.LogAuthState($"Sent ServerLogonProof {(success ? "successful" : "failed")}");
            //        if (success)
            //        {
            //            this.state = ClientState.Authenticated;
            //            this.LogAuthState("Client authenticated");
            //        }
            //        else
            //        {
            //            this.state = ClientState.Disconnected;
            //            this.isConnected = false;
            //            this.LogAuthState("Client authentication failed");
            //        }
            //        break;
            //    case ClientState.Authenticated:
            //        DataStore.Users.TryAdd(this.SRP.I, new User(this.SRP));
            //        this.LogAuthState("Sent Realmlist");
            //        await ServerRealmList.Send(this);
            //        break;
            //}
        }

        private void LogAuthState(string message) => this.logger.LogTrace($"{this.ClientInfo} - {message}");
    }
}