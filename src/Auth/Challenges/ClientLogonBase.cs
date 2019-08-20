using System.Threading.Tasks;

namespace Classic.Auth.Challenges
{
    public abstract class ClientLogonBase
    {
        protected readonly byte[] packet;
        protected readonly LoginClient client;

        public ClientLogonBase(byte[] packet, LoginClient client)
        {
            this.packet = packet;
            this.client = client;
        }

        public abstract Task<bool> Execute();
    }
}