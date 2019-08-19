using System.Net;
using System.Net.Sockets;

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

        public void Start()
        {
            this.server.Start();
            this.isActive = true;
            while (this.isActive)
            {
                var client = this.server.AcceptTcpClient();
                this.ProcessClient(client);
            }
        }

        public void Stop()
        {
            this.isActive = false;
            this.server.Stop();
        }

        protected abstract void ProcessClient(TcpClient client);
    }
}