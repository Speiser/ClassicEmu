using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Classic.Common;

namespace Classic.World
{
    public class WorldPacketHandler
    {
        public delegate Task PacketHandler(WorldClient client, byte[] data);

        private static readonly Dictionary<Opcode, PacketHandler> PacketHandlers = new Dictionary<Opcode, PacketHandler>();

        public static void Initialize()
        {
            var methods = Assembly.GetExecutingAssembly()
                .GetTypes()
                .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                .Where(m => m.GetCustomAttributes<OpcodeHandlerAttribute>().Any());

            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes<OpcodeHandlerAttribute>();
                foreach (var attribute in attributes)
                {
                    PacketHandlers.Add(attribute.Opcode, (client, data) => (Task)method.Invoke(null, new object[] { client, data }));
                }
            }
        }

        public static PacketHandler GetHandler(Opcode opcode)
        {
            return PacketHandlers.TryGetValue(opcode, out var handler)
                ? handler
                : (client, data) => {
                    Logger.Log($"Unhandled opcode {opcode}.");
                    return Task.CompletedTask;
                };
        }
    }
}
