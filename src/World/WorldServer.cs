using Classic.Common;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Classic.World
{
    public class WorldServer : ServerBase
    {
        public WorldServer() : base(new IPEndPoint(IPAddress.Loopback, 13250))
        {
            WorldPacketHandler.Initialize();
        }

        protected override void ProcessClient(TcpClient client)
        {
            new Thread(() => new WorldClient(client).HandleConnection()).Start();
        }
    }
}
