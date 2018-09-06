using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Classic.Common
{
    public abstract class ClientBase
    {
        protected bool isConnected; // TODO: Replace with cancellationtoken
        private TcpClient client;
        private NetworkStream stream;

        public ClientBase(TcpClient client)
        {
            this.client = client;
            this.stream = client.GetStream();
            this.isConnected = true;
            var endPoint = (IPEndPoint)client.Client.RemoteEndPoint;
            this.ClientInfo = endPoint.Address + ":" + endPoint.Port;
        }

        public string ClientInfo { get; }

        public void HandleConnection()
        {
            while (this.isConnected)
            {
                var buffer = new byte[1024];
                var length = this.stream.Read(buffer, 0, buffer.Length);

                if (length == 0)
                {
                    this.Log($"-- disconnected");
                    this.isConnected = false;
                    break;
                }

                this.HandlePacket(buffer.Take(length).ToArray());
            }
        }

        public void Send(byte[] data)
        {
            if (!this.isConnected)
                throw new InvalidOperationException($"Client {this.ClientInfo} is not connected.");

            this.stream.Write(data, 0, data.Length);
        }

        protected void Log(string message)
        {
            Logger.Log($"[{this.ClientInfo}] [{this.GetType().Name}] " + message);
        }

        protected void LogPacket(byte[] packet)
        {
            Logger.Log($"[{this.ClientInfo}] [{this.GetType().Name}] Packet received {packet.Length} bytes");
        }

        protected abstract void HandlePacket(byte[] packet);
    }
}