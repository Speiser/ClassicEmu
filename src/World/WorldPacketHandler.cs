using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Classic.World.Extensions;

namespace Classic.World
{
    public class WorldPacketHandler
    {
        public delegate Task PacketHandler(WorldClient client, byte[] data);

        private readonly Dictionary<Opcode, PacketHandler> handlers = new Dictionary<Opcode, PacketHandler>();
        private readonly ILogger<WorldPacketHandler> logger;

        public WorldPacketHandler(ILogger<WorldPacketHandler> logger)
        {
            this.logger = logger;

            var methods = Assembly.GetExecutingAssembly()
                .GetTypes()
                .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                .Where(m => m.GetCustomAttributes<OpcodeHandlerAttribute>().Any());

            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes<OpcodeHandlerAttribute>();
                foreach (var attribute in attributes)
                {
                    handlers.Add(attribute.Opcode, (client, data) => (Task)method.Invoke(null, new object[] { client, data }));
                }
            }
        }

        public PacketHandler GetHandler(Opcode opcode)
        {
            return handlers.TryGetValue(opcode, out var handler)
                ? handler
                : (client, data) => {
                    logger.LogUnhandledOpcode(opcode);
                    return Task.CompletedTask;
                };
        }
    }
}
