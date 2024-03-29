using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Classic.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Classic.Auth;

public class AuthenticationServer : BackgroundService
{
    private readonly TcpListener server;
    private readonly ILogger<AuthenticationServer> logger;
    private readonly IServiceProvider services;
    private readonly AccountService accountService;
    private readonly RealmlistService realmlistService;
    private readonly AuthDatabase authDatabase;

    public AuthenticationServer(
        IServiceProvider services,
        ILogger<AuthenticationServer> logger,
        AccountService accountService,
        RealmlistService realmlistService,
        AuthDatabase authDatabase)
    {
        this.server = new TcpListener(new IPEndPoint(IPAddress.Loopback, 3724));
        this.logger = logger;

        this.services = services;
        this.accountService = accountService;
        this.realmlistService = realmlistService;
        this.authDatabase = authDatabase;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        this.server.Stop();
        return base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await this.authDatabase.Initialize();
        await this.realmlistService.Clear(); // TODO: not needed (should be done via online/offline flag?)
        this.server.Start();
        while (!cancellationToken.IsCancellationRequested)
        {
            var client = await this.server.AcceptTcpClientAsync();
            _ = Task.Run(async () =>
            {
                try
                {
                    var loginClient = services.GetService<LoginClient>();
                    await loginClient.Initialize(client);
                }
                catch (Exception e)
                {
                    logger.LogError(e.ToString());
                }
            }, cancellationToken);
        }
    }
}
