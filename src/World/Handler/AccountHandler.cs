using System.Threading.Tasks;
using Classic.World.Messages.Client;

namespace Classic.World.Handler
{
    public class AccountHandler
    {
        [OpcodeHandler(Opcode.CMSG_UPDATE_ACCOUNT_DATA)]
        public static Task UpdateAccountData(WorldClient client, byte[] data)
        {
            var request = new CMSG_UPDATE_ACCOUNT_DATA(data);
            return Task.CompletedTask;
        }
    }
}
