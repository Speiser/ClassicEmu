using System.Threading.Tasks;
using Classic.Auth.Challenges.Abstract;

namespace Classic.Auth.Challenges
{
    public class ClientReconnectChallenge : ClientChallengeBase
    {
        public ClientReconnectChallenge(byte[] packet, LoginClient client) : base(packet, client) { }

        public override async Task<bool> Execute()
        {
            await this.client.Send(new ServerReconnectChallenge().Get());
            return true;
        }
    }
}