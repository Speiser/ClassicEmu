using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Classic.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Classic.Auth
{
    public class AuthenticationServer : ServerBase
    {
        private readonly IServiceProvider services;

        public AuthenticationServer(IServiceProvider services, ILogger<AuthenticationServer> logger)
            : base(new IPEndPoint(IPAddress.Loopback, 3724), logger)
        {
            this.services = services;
        }

        protected override async Task ProcessClient(TcpClient client)
        {
            var loginClient = services.GetService<LoginClient>();
            await loginClient.Initialize(client);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await DataStore.Save();
            await base.StopAsync(cancellationToken);
        }
    }
}