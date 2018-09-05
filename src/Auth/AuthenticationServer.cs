using System.Net;
using System.Net.Sockets;
using System.Threading;

using Classic.Common;

namespace Classic.Auth
{
    public class AuthenticationServer : ServerBase
    {
        public AuthenticationServer() : base(new IPEndPoint(IPAddress.Loopback, 3724)) { }

        protected override void ProcessClient(TcpClient client)
        {
            new Thread(() => {
                 new LoginClient(client).HandleConnection();
            }).Start();
        }
    }
} // asdfghjk