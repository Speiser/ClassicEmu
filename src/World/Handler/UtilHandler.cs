using System.Threading.Tasks;
using Classic.Common;
using Classic.World.Messages.Client;
using Classic.World.Messages.Server;

namespace Classic.World.Handler
{
    public class UtilHandler
    {
        [OpcodeHandler(Opcode.CMSG_PING)]
        public static async Task OnPing(HandlerArguments args)
        {
            var request = new CMSG_PING(args.Data);
            await args.Client.SendPacket(new SMSG_PONG(request.Latency));
        }

        [OpcodeHandler(Opcode.CMSG_NAME_QUERY)]
        public static async Task OnNameQuery(HandlerArguments args)
        {
            var request = new CMSG_NAME_QUERY(args.Data);
            var character = DataStore.GetCharacter(request.CharacterID);
            if (character is null)
                return;
            await args.Client.SendPacket(new SMSG_NAME_QUERY_RESPONSE(character));
        }

        [OpcodeHandler(Opcode.CMSG_QUERY_TIME)]
        public static async Task OnQueryTime(HandlerArguments args) => await args.Client.SendPacket(new SMSG_QUERY_TIME_RESPONSE());
    }
}
