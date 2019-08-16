using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Classic.Common;

namespace Classic.World
{
    public class WorldPacketHandler
    {
        public delegate void PacketHandler(WorldClient client, byte[] data);

        private static ImmutableDictionary<Opcode, PacketHandler> PacketHandlers;

        public static void Initialize()
        {
            PacketHandlers = Assembly.GetExecutingAssembly()
                .GetTypes()
                .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                .Where(m => m.GetCustomAttributes<OpcodeHandlerAttribute>().Any())
                .ToImmutableDictionary<MethodInfo, Opcode, PacketHandler>(
                    m => m.GetCustomAttribute<OpcodeHandlerAttribute>().Opcode,
                    m => (client, data) => m.Invoke(null, new object[] { client, data }));
        }

        public static PacketHandler GetHandler(Opcode opcode)
        {
            return PacketHandlers.TryGetValue(opcode, out var handler)
                ? handler
                : (client, data) => Logger.Log($"Unhandled opcode {opcode}.");
        }
    }
}
