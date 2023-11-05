using System.Diagnostics;
using System.Threading.Tasks;
using Classic.World.Packets;
using Classic.World.Packets.Client;

namespace Classic.World.Handler;

class ZoneHandler
{
    [OpcodeHandler(Opcode.CMSG_ZONEUPDATE)]
    public static async Task OnZoneUpdate(PacketHandlerContext c)
    {
        var request = new CMSG_ZONEUPDATE(c.Packet);
        var character = await c.World.CharacterService.GetCharacter(c.Client.CharacterId);
        Debug.Assert(character is not null);
        c.Client.Log($"{character.Name} entered {request.NewZone}");
        character.Position.Zone = request.NewZone;
    }
}
