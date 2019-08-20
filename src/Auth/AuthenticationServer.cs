using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Classic.Common;

namespace Classic.Auth
{
    public class AuthenticationServer : ServerBase
    {
        public AuthenticationServer() : base(new IPEndPoint(IPAddress.Loopback, 3724)) { }

        protected override async Task ProcessClient(TcpClient client)
        {
            var loginClient = new LoginClient(client);
            await loginClient.HandleConnection();
        }
    }
}