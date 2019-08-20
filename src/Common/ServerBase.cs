using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Classic.Common
{
    public abstract class ServerBase
    {
        private bool isActive; // TODO: replace with cancellationtoken
        private readonly TcpListener server;
        
        public ServerBase(IPEndPoint endPoint)
        {
            this.server = new TcpListener(endPoint);
        }

        public async Task Start()
        {
            this.server.Start();
            this.isActive = true;
            while (this.isActive)
            {
                var client = await this.server.AcceptTcpClientAsync();
                _ = this.ProcessClient(client);
            }
        }

        public void Stop()
        {
            this.isActive = false;
            this.server.Stop();
        }

        protected abstract Task ProcessClient(TcpClient client);
    }
}