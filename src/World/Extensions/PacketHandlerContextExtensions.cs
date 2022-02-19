using System.Threading.Tasks;
using Classic.Shared.Data;
using Classic.World.Data;
using Classic.World.Packets;

namespace Classic.World.Extensions;

internal static class PacketHandlerContextExtensions
{
    public static bool IsVanilla(this PacketHandlerContext c) => c.Client.Build == ClientBuild.Vanilla;
    public static bool IsTBC(this PacketHandlerContext c) => c.Client.Build == ClientBuild.TBC;
    public static Character GetCharacter(this PacketHandlerContext c) => c.World.CharacterService.GetCharacter(c.Client.CharacterId);
    public static Character GetCharacter(this PacketHandlerContext c, string name) => c.World.CharacterService.GetCharacter(name);
    public static Character GetCharacter(this PacketHandlerContext c, ulong id) => c.World.CharacterService.GetCharacter(id);


    public static async Task SendPacket<T>(this PacketHandlerContext c) where T : ServerPacketBase<Opcode>, new()
        => await c.Client.SendPacket(new T());
}
