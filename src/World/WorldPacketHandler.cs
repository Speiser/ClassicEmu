using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Classic.World.Extensions;
using Classic.World.Messages;
using Microsoft.Extensions.Logging;

namespace Classic.World
{
    public class WorldPacketHandler
    {
        public delegate Task PacketHandler(PacketHandlerContext args);

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
                    handlers.Add(attribute.Opcode, args => (Task)method.Invoke(null, new object[] { args }));
                }
            }
        }

        public PacketHandler GetHandler(Opcode opcode)
        {
            return handlers.TryGetValue(opcode, out var handler)
                ? handler
                : args => {
                    logger.LogUnhandledOpcode(opcode);
                    return Task.CompletedTask;
                };
        }
    }
}
