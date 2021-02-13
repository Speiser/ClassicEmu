using System;
using System.Collections.Generic;
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
            this.CurrentCreatures.Add(new Creature { ID = Cryptography.Random.GetUInt64(), Model = 169, Position = Map.StartingAreas[Race.NightElf] });
        }

        public List<Character> CurrentPlayers { get; } = new List<Character>();
        public List<Creature> CurrentCreatures { get; } = new List<Creature>();

        protected override async Task ProcessClient(TcpClient client)
        {
            var worldClient = services.GetService<WorldClient>();
            await worldClient.Initialize(client);
        }
    }
}
