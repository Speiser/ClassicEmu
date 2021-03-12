using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Classic.Shared
{
    public abstract class ServerBase : BackgroundService
    {
        private readonly TcpListener server;
        private readonly ILogger<ServerBase> logger;

        public ServerBase(IPEndPoint endPoint, ILogger<ServerBase> logger)
        {
            this.server = new TcpListener(endPoint);
            this.logger = logger;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this.server.Stop();
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            this.server.Start();
            while (!cancellationToken.IsCancellationRequested)
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
                }, cancellationToken);
            }
        }

        protected abstract Task ProcessClient(TcpClient client);
    }
}