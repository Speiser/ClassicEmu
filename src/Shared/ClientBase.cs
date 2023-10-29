using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Classic.Shared;

public abstract class ClientBase
{
    protected readonly ILogger<ClientBase> logger;
    protected bool isConnected;
    private NetworkStream stream;

    public ClientBase(ILogger<ClientBase> logger)
    {
        this.logger = logger;
    }

    // TODO: Non-virtual
    public virtual Task Initialize(TcpClient client, int build = 0)
    {
        this.stream = client.GetStream();
        this.isConnected = true;
        var endPoint = (IPEndPoint)client.Client.RemoteEndPoint;
        this.Address = endPoint.Address.ToString();
        this.Port = endPoint.Port;
        this.ClientInfo = endPoint.Address + ":" + endPoint.Port;
        return Task.CompletedTask;
    }

    public string Address { get; private set; }
    public int Port { get; private set; }

    public string ClientInfo { get; private set; }

    protected async Task HandleConnection()
    {
        while (this.isConnected)
        {
            await this.HandleIncomingPacket();
        }

        OnDisconnected();
    }

    public async Task Send(byte[] data)
    {
        if (!this.isConnected)
            throw new InvalidOperationException($"Client {this.ClientInfo} is not connected.");

        await this.stream.WriteAsync(data.AsMemory(0, data.Length));
    }

    public void Log(string message, LogLevel level = LogLevel.Debug)
    {
        this.logger.Log(level, $"{this.ClientInfo} - {message}");
    }

    protected ValueTask<int> ReadInto(byte[] buffer) => this.stream.ReadAsync(buffer.AsMemory(0, buffer.Length));

    protected virtual void OnDisconnected() { }
    protected abstract Task HandleIncomingPacket();
}
