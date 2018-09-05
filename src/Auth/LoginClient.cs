using System.Net.Sockets;

using Classic.Auth.Challenges;
using Classic.Common;
using Classic.Cryptography;

namespace Classic.Auth
{
    public class LoginClient : ClientBase
    {
        private ClientState state;
        public LoginClient(TcpClient client) : base(client)
        {
            Logger.Log($"[{this.ClientInfo}] <> connected");
            this.state = ClientState.Init;
        }

        public SecureRemotePasswordProtocol SRP { get; internal set; }

        protected override void HandlePacket(byte[] packet)
        {
            Logger.LogPacket(packet);
            switch (this.state)
            {
                case (ClientState.Init):
                    this.state = ClientState.ClientLogonChallenge;
                    Logger.Log($"[{this.ClientInfo}] -> ClientLogonChallenge");
                    new ClientLogonChallenge(packet, this).Execute();
                    this.state = ClientState.ServerLogonChallenge;
                    Logger.Log($"[{this.ClientInfo}] <- ServerLogonChallenge");
                    break;
                case (ClientState.ServerLogonChallenge):
                    this.state = ClientState.ClientLogonProof;
                    Logger.Log($"[{this.ClientInfo}] -> ClientLogonProof");
                    var success = new ClientLogonProof(packet, this).Execute();
                    Logger.Log($"[{this.ClientInfo}] <- ServerLogonProof {(success ? "successful" : "failed")}");
                    if (success)
                    {
                        this.state = ClientState.Authenticated;
                        Logger.Log($"[{this.ClientInfo}] <> Client authenticated");
                    }
                    else
                    {
                        this.state = ClientState.Disconnected;
                        this.isConnected = false;
                        Logger.Log($"[{this.ClientInfo}] <> Client authentication failed");
                    }
                    break;
                case (ClientState.Authenticated):
                    Logger.Log("REALMLIST WOOHOHHO");
                    new ServerRealmList().Send(this);
                    break;
            }
        }
    }
}