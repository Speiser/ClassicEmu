using System.Threading.Tasks;
using Classic.Auth.Challenges.Abstract;
using Classic.Shared;
using Classic.Auth.Cryptography;

namespace Classic.Auth.Challenges
{
    public class ClientLogonChallenge : ClientChallengeBase
    {
        public ClientLogonChallenge(byte[] packet, LoginClient client) : base(packet, client) { }

        public override async Task<bool> Execute()
        {
            this.client.SRP = new SecureRemotePasswordProtocol(this.identifier, this.identifier); // TODO: Quick hack
            AccountStore.AddClientBuildForAddress(this.client.Address, this.client.Port, this.build);

            // Create and send a ServerLogonChallenge as response.
            await this.client.Send(new ServerLogonChallenge(this.client.SRP).Get());
            return true;
        }
    }
}