using System.Threading.Tasks;
using Classic.World.Extensions;
using Classic.World.Packets;
using Classic.World.Packets.Client;

namespace Classic.World.Handler
{
    class ZoneHandler
    {
        [OpcodeHandler(Opcode.CMSG_ZONEUPDATE)]
        public static Task OnZoneUpdate(PacketHandlerContext c)
        {
            var request = new CMSG_ZONEUPDATE(c.Packet);
            c.Client.Log($"{c.Client.Player.Name} entered {request.NewZone}");
            c.GetCharacter().Position.Zone = request.NewZone;
            return Task.CompletedTask;
        }
    }
}
