using System;
using System.Net;
using System.Net.Sockets;
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

        public Server()
        {
            _ip = IPAddress.Loopback;
            _port = 5001;
            _listener = new TcpListener(_ip, _port);
        }

        public void LogonProcess()
        {
            // http://arcemu.org/wiki/Logon_Process.
            _listener.Start();
            var client = _listener.AcceptTcpClient();
            var stream = client.GetStream();
            var buffer = new byte[8192 * 10];
            var len = stream.Read(buffer, 0, buffer.Length);
            var data = new byte[len];
            for (int i = 0; i < len; i++)
            {
                data[i] = buffer[i];
            }

            Console.WriteLine();
            ClientLogonChallenge clc = new ClientLogonChallenge()
            {
                cmd = data[0],
                error = data[1],
                size = BitConverter.ToUInt16(new byte[2] { data[2], data[3] }, 0),
                gamename = new byte[4] { data[4], data[5], data[6], data[7] },
                version1 = data[8],
                version2 = data[9],
                version3 = data[10],
                build = BitConverter.ToUInt16(new byte[2] { data[11], data[12] }, 0),
                platform = new byte[4] { data[13], data[14], data[15], data[16] },
                os = new byte[4] { data[17], data[18], data[19], data[20] },
                country = new byte[4] { data[21], data[22], data[23], data[24] },
                timezone_bias = BitConverter.ToUInt32(new byte[4] { data[25], data[26], data[27], data[28] }, 0),
                ip = BitConverter.ToUInt32(new byte[4] { data[29], data[30], data[31], data[32] }, 0),
                I_len = data[33],
                I = new byte[data[33]]
            };

            for (int i = 0; i < clc.I_len; i++)
            {
                clc.I[i] = data[i + 34];
            }

            Console.WriteLine(clc.ToString());
        }
    }
}
