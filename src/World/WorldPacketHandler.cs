using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Classic.Common;

namespace Classic.World
{
    public class WorldPacketHandler
    {
        public delegate void PacketHandler(WorldClient client, byte[] data);

        private static ConcurrentDictionary<Opcode, PacketHandler> PacketHandlers { get; }
            = new ConcurrentDictionary<Opcode, PacketHandler>();

        public static void Register(Opcode opcode, PacketHandler handler)
        {
            if (!PacketHandlers.TryAdd(opcode, handler))
            {
                throw new InvalidOperationException($"Handler for {opcode} already registered.");
            }
        }

        public static PacketHandler GetHandler(Opcode opcode)
        {
            return PacketHandlers.TryGetValue(opcode, out var handler)
                ? handler
                : (client, data) => Logger.Log($"Unhandled cmd {opcode}.");
        }
    }
}
