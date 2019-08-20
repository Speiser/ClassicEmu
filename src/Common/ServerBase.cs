using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Classic.Common
{
    public abstract class ServerBase
    {
        private bool isActive; // TODO: replace with cancellationtoken
        private readonly TcpListener server;
        private readonly ILogger<ServerBase> logger;

        public ServerBase(IPEndPoint endPoint, ILogger<ServerBase> logger)
        {
            this.server = new TcpListener(endPoint);
            this.logger = logger;
        }

        public async Task Start()
        {
            this.server.Start();
            this.isActive = true;
            while (this.isActive)
            {
                var client = await this.server.AcceptTcpClientAsync();
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await this.ProcessClient(client);
                    }
                    catch (Exception e)
                    {
                        logger.LogError(e.ToString());
                    }
                });
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