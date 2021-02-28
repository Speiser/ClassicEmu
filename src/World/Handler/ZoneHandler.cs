using System.Threading.Tasks;
using Classic.World.Messages.Client;

namespace Classic.World.Handler
{
    class ZoneHandler
    {
        [OpcodeHandler(Opcode.CMSG_ZONEUPDATE)]
        public static Task OnZoneUpdate(HandlerArguments args)
        {
            var request = new CMSG_ZONEUPDATE(args.Data);
            args.Client.Log($"{args.Client.Player.Name} entered {request.NewZone}");
            args.Client.Character.Position.Zone = request.NewZone;
            return Task.CompletedTask;
        }
    }
}
