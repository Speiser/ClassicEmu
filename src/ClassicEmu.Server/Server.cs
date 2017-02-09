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
            Console.WriteLine(clc);

            // Server Logon Challenge:
            //byte[] slc = LogonMessageCreator.CreateServerLogonChallenge(clc);
            //stream.Write(slc, 0, slc.Length);
            byte[] a = new byte[119] {
            0x00, 0x00, 0x00, 0x72, 0x50, 0xa7, 0xc9, 0x27, 0x4a, 0xfa, 0xb8, 0x77, 0x80, 0x70, 0x22,
            0xda, 0xb8, 0x3b, 0x06, 0x50, 0x53, 0x4a, 0x16, 0xe2, 0x65, 0xba, 0xe4, 0x43, 0x6f, 0xe3,
            0x29, 0x36, 0x18, 0xe3, 0x45, 0x01, 0x07, 0x20, 0x89, 0x4b, 0x64, 0x5e, 0x89, 0xe1, 0x53,
            0x5b, 0xbd, 0xad, 0x5b, 0x8b, 0x29, 0x06, 0x50, 0x53, 0x08, 0x01, 0xb1, 0x8e, 0xbf, 0xbf,
            0x5e, 0x8f, 0xab, 0x3c, 0x82, 0x87, 0x2a, 0x3e, 0x9b, 0xb7, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xe1, 0x32, 0xa3,
            0x49, 0x76, 0x5c, 0x5b, 0x35, 0x9a, 0x93, 0x3c, 0x6f, 0x3c, 0x63, 0x6d, 0xc0, 0x00
            };
            stream.Write(a, 0, a.Length);

            // Client Logon Proof:
            byte[] recv = this.ReadStream(75, stream);
            Console.WriteLine(Encoding.ASCII.GetString(recv));

            // Server Logon Proof:
        }

        private byte[] ReadStream(int length, NetworkStream stream)
        {
            byte[] data = new byte[0];
            int len = 0;

            while (len == 0)
            {
                var buffer = new byte[length];
                len = stream.Read(buffer, 0, buffer.Length);
                data = new byte[len];

                for (int i = 0; i < len; i++)
                {
                    data[i] = buffer[i];
                }

                Thread.Sleep(100);
            }

            return data;
        }
    }
}
