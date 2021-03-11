using System.Threading.Tasks;
using Classic.Shared.Data;
using Classic.World.Messages;

namespace Classic.World.Extensions
{
    internal static class PacketHandlerContextExtensions
    {
        public static bool IsVanilla(this PacketHandlerContext c) => c.Client.Build == ClientBuild.Vanilla;
        public static bool IsTBC(this PacketHandlerContext c) => c.Client.Build == ClientBuild.TBC;

        public static async Task SendPacket<T>(this PacketHandlerContext c) where T : ServerMessageBase<Opcode>, new()
            => await c.Client.SendPacket(new T());
    }
}
