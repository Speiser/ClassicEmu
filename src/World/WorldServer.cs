using Classic.Common;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Classic.World
{
    public class WorldServer : ServerBase
    {
        public WorldServer() : base(new IPEndPoint(IPAddress.Loopback, 13250))
        {
            WorldPacketHandler.Initialize();
        }

        protected override async Task ProcessClient(TcpClient client)
        {
            var worldClient = new WorldClient(client);
            await worldClient.HandleConnection();
        }
    }
}
