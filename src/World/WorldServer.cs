using Classic.Common;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Classic.World
{
    public class WorldServer : ServerBase
    {
        private readonly IServiceProvider services;

        public WorldServer(IServiceProvider services, ILogger<WorldServer> logger) : base(new IPEndPoint(IPAddress.Loopback, 13250), logger)
        {
            this.services = services;
        }

        protected override async Task ProcessClient(TcpClient client)
        {
            var worldClient = services.GetService<WorldClient>();
            await worldClient.Initialize(client);
        }
    }
}
