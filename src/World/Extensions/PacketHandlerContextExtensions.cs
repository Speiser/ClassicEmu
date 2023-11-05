using System.Linq;
using System.Threading.Tasks;
using Classic.Shared.Data;
using Classic.World.Packets;

namespace Classic.World.Extensions;

internal static class PacketHandlerContextExtensions
{
    public static bool IsVanilla(this PacketHandlerContext c) => c.Client.Build == ClientBuild.Vanilla;
    public static bool IsTBC(this PacketHandlerContext c) => c.Client.Build == ClientBuild.TBC;

    public static bool TrySetTarget(this PacketHandlerContext c, ulong targetId)
    {
        if (targetId == 0)
        {
            return false;
        }

        var unit = c.World.Creatures.SingleOrDefault(creature => creature.ID == targetId);

        if (unit is null)
        {
            c.Client.Log($"Could not find unit: {targetId}");
            return false;
        }

        c.Client.Player.Target = unit;
        return true;
    }


    public static async Task SendPacket<T>(this PacketHandlerContext c) where T : ServerPacketBase<Opcode>, new()
        => await c.Client.SendPacket(new T());
}
