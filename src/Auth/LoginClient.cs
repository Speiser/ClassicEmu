using System.Net.Sockets;
using Classic.Auth.Challenges;
using Classic.Common;
using Classic.Cryptography;
using Classic.Data;

namespace Classic.Auth
{
    public class LoginClient : ClientBase
    {
        private ClientState state;
        public LoginClient(TcpClient client) : base(client)
        {
            this.Log("-- connected");
            this.state = ClientState.Init;
        }

        public SecureRemotePasswordProtocol SRP { get; internal set; }

        protected override void HandlePacket(byte[] packet)
        {
            this.LogPacket(packet);
            switch (this.state)
            {
                case (ClientState.Init):
                    this.state = ClientState.LogonChallenge;
                    this.Log("-> ClientLogonChallenge");
                    new ClientLogonChallenge(packet, this).Execute();
                    this.Log("<- ServerLogonChallenge");
                    break;
                case (ClientState.LogonChallenge):
                    this.state = ClientState.LogonProof;
                    this.Log("-> ClientLogonProof");
                    var success = new ClientLogonProof(packet, this).Execute();
                    this.Log($"<- ServerLogonProof {(success ? "successful" : "failed")}");
                    if (success)
                    {
                        this.state = ClientState.Authenticated;
                        this.Log("-- Client authenticated");
                    }
                    else
                    {
                        this.state = ClientState.Disconnected;
                        this.isConnected = false;
                        this.Log("-- Client authentication failed");
                    }
                    break;
                case (ClientState.Authenticated):
                    DataStore.Users.TryAdd(this.SRP.I, new User(this.SRP));
                    this.Log("<- Realmlist sent");
                    ServerRealmList.Send(this);
                    break;
            }
        }
    }
}