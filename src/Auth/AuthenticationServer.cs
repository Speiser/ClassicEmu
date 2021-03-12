using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Classic.Shared;
using Classic.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Classic.Auth
{
    public class AuthenticationServer : ServerBase
    {
        private readonly IServiceProvider services;
        private readonly AccountService accountService;

        public AuthenticationServer(
            IServiceProvider services,
            ILogger<AuthenticationServer> logger,
            AccountService accountService,
            RealmlistService realmlistService)
            : base(new IPEndPoint(IPAddress.Loopback, 3724), logger)
        {
            realmlistService.Clear();
            this.services = services;
            this.accountService = accountService;
        }

        protected override async Task ProcessClient(TcpClient client)
        {
            var loginClient = services.GetService<LoginClient>();
            await loginClient.Initialize(client);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            this.accountService.ClearAccountSessions();
            await base.StopAsync(cancellationToken);
        }
    }
}