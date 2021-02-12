using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Classic.Common
{
    public abstract class ClientBase
    {
        protected readonly ILogger<ClientBase> logger;
        protected bool isConnected; // TODO: Replace with cancellationtoken
        private NetworkStream stream;

        public ClientBase(ILogger<ClientBase> logger)
        {
            this.logger = logger;
        }

        public virtual Task Initialize(TcpClient client)
        {
            this.stream = client.GetStream();
            this.isConnected = true;
            var endPoint = (IPEndPoint)client.Client.RemoteEndPoint;
            this.ClientInfo = endPoint.Address + ":" + endPoint.Port;
            return Task.CompletedTask;
        }

        public string ClientInfo { get; private set; }

        protected async Task HandleConnection()
        {
            while (this.isConnected)
            {
                // Biggest package seen so far was 1190 bytes in length
                // Incase many packets are sent by the client, the server currently
                // dies with 2048 bytes, so 4096 are used for now...
                var buffer = new byte[4096];
                var length = await this.stream.ReadAsync(buffer, 0, buffer.Length);

                if (length == 0)
                {
                    this.Log($"-- disconnected");
                    this.isConnected = false;
                    break;
                }

                // Do not await
                try
                {
                    await this.HandlePacket(buffer.Take(length).ToArray());
                }
                catch (Exception e)
                {
                    logger.LogError(e.ToString());
                }
            }

            OnDisconnected();
        }

        public async Task Send(byte[] data)
        {
            if (!this.isConnected)
                throw new InvalidOperationException($"Client {this.ClientInfo} is not connected.");

            await this.stream.WriteAsync(data, 0, data.Length);
        }

        public void Log(string message)
        {
            logger.LogDebug($"[{this.ClientInfo}] [{this.GetType().Name}] " + message);
        }

        // Todo change to abstract later
        protected virtual void OnDisconnected() { }

        protected void LogPacket(byte[] packet)
        {
            this.Log($"Packet received {packet.Length} bytes");
        }

        protected abstract Task HandlePacket(byte[] packet);
    }
}