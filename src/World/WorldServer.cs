using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Classic.Common;
using Classic.Data;
using Classic.Data.Enums.Character;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Classic.World
{
    public class WorldServer : ServerBase
    {
        private readonly IServiceProvider services;

        public WorldServer(IServiceProvider services, ILogger<WorldServer> logger) : base(new IPEndPoint(IPAddress.Loopback, 13250), logger)
        {
            this.services = services;
            this.State.Creatures.Add(new Creature { Model = 169, Position = Map.StartingAreas[Race.NightElf] });
        }

        public WorldState State { get; } = new WorldState();

        protected override async Task ProcessClient(TcpClient client)
        {
            var worldClient = services.GetService<WorldClient>();
            this.State.Connections.Add(worldClient);
            await worldClient.Initialize(client);
        }
    }
}
