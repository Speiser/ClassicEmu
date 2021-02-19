using System.Threading.Tasks;

namespace Classic.Auth.Challenges.Abstract
{
    public abstract class ClientMessageBase
    {
        protected readonly byte[] packet;
        protected readonly LoginClient client;

        public ClientMessageBase(byte[] packet, LoginClient client)
        {
            this.packet = packet;
            this.client = client;
        }

        public abstract Task<bool> Execute();
    }
}