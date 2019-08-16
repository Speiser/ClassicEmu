using Classic.Common;
using Classic.World.Entities;
using System.Collections.Concurrent;
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

        public ConcurrentBag<PlayerEntity> ActivePlayers { get; } = new ConcurrentBag<PlayerEntity>();

        protected override void ProcessClient(TcpClient client)
        {
            var worldClient = new WorldClient(client);
            worldClient.PlayerSpawned += player => ActivePlayers.Add(player);
            worldClient.PlayerDespawned += player => ActivePlayers.TryTake(out player);

            new Thread(() => worldClient.HandleConnection()).Start();
        }
    }
}
