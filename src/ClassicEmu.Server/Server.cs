using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ClassicEmu.Server.Structs;
using ClassicEmu.Shared;

namespace ClassicEmu.Server
{
    [Status("Prototype")]
    public class Server
    {
        private IPAddress _ip;
        private int _port;
        private TcpListener _listener;

        public Server(IPAddress ip, int port = 5001)
        {
            _ip = ip;
            _port = port;
            _listener = new TcpListener(_ip, _port);
            this.IsActive = false;
        }

        public bool IsActive { get; private set; }

        public void Start()
        {
            if (this.IsActive)
                return;

            _listener.Start();
            this.IsActive = true;
            new Thread(this.WaitForConnection).Start();
        }

        public void Stop()
        {
            if (!this.IsActive)
                return;

            _listener.Stop();
            this.IsActive = false;
        }

        private void WaitForConnection()
        {
            while (this.IsActive)
            {
                var client = _listener.AcceptTcpClient();
                new Thread(() => this.LogonProcess(client)).Start();
            }
        }

        // Based on http://arcemu.org/wiki/Logon_Process.
        [Status("Server Logon Challenge Fails")]
        private void LogonProcess(TcpClient client)
        {
            var stream = client.GetStream();

            // Client Logon Challenge:
            byte[] data = this.ReadStream(83, stream);
            ClientLogonChallenge clc = LogonMessageCreator.CreateClientLogonChallenge(data);

            // Server Logon Challenge:
            byte[] slc = LogonMessageCreator.CreateServerLogonChallenge(clc);
            stream.Write(slc, 0, slc.Length);

            // Client Logon Proof:
            byte[] recv = this.ReadStream(75, stream);
            Console.WriteLine(Encoding.ASCII.GetString(recv));

            // Server Logon Proof:
        }

        private byte[] ReadStream(int length, NetworkStream stream)
        {
            var buffer = new byte[length];
            int len = stream.Read(buffer, 0, buffer.Length);
            var data = new byte[len];

            for (int i = 0; i < len; i++)
            {
                data[i] = buffer[i];
            }

            return data;
        }
    }
}
