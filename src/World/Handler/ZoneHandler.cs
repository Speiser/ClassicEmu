using System.Threading.Tasks;
using Classic.Data.Enums.Map;
using Classic.World.Messages.Client;

namespace Classic.World.Handler
{
    class ZoneHandler
    {
        [OpcodeHandler(Opcode.CMSG_ZONEUPDATE)]
        public static Task OnZoneUpdate(WorldClient client, byte[] data)
        {
            var request = new CMSG_ZONEUPDATE(data);

            client.Log($"{client.Player.Name} entered {(ZoneID)request.NewZone}");

            return Task.CompletedTask;
        }
    }
}
