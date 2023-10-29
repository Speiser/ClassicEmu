using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Classic.World;

public class VersionServer
{
    private readonly TcpListener server;
    private readonly int build;
    private readonly ILogger<WorldServer> logger; // TODO: Correct generic
    private readonly Func<TcpClient, int, Task> processClient;

    public VersionServer(IPEndPoint endPoint, int build, Func<TcpClient, int, Task> processClient, ILogger<WorldServer> logger)
    {
        this.server = new TcpListener(endPoint);
        this.build = build;
        this.processClient = processClient;
        this.logger = logger;
    }

    public void Stop()
    {
        this.server.Stop();
        this.logger.LogInformation($"Stopped {((IPEndPoint)this.server.LocalEndpoint).Port}");
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        this.server.Start();
        this.logger.LogInformation($"Started {((IPEndPoint)this.server.LocalEndpoint).Port}");
        while (!cancellationToken.IsCancellationRequested)
        {
            var client = await this.server.AcceptTcpClientAsync();
            _ = Task.Run(async () =>
            {
                try
                {
                    await this.processClient(client, this.build);
                }
                catch (Exception e)
                {
                    logger.LogError(e.ToString());
                }
            }, cancellationToken);
        }
    }
}