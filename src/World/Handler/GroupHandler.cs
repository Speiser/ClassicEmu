using System.Linq;
using System.Threading.Tasks;
using Classic.World.Extensions;
using Classic.World.Packets;
using Classic.World.Packets.Client;
using Classic.World.Packets.Server;
using Microsoft.Extensions.Logging;

namespace Classic.World.Handler;

public class GroupHandler
{
    [OpcodeHandler(Opcode.CMSG_GROUP_INVITE)]
    public static async Task GroupInvite(PacketHandlerContext c)
    {
        var request = new CMSG_GROUP_INVITE(c.Packet);
        var recipient = c.GetCharacter(request.Membername);
        if (recipient is null)
        {
            c.Client.Log($"Could not find player {request.Membername}", LogLevel.Warning);
            return; // TODO Send response to sender
        }

        var recipientClient = c.World.Connections.SingleOrDefault(c => c.CharacterId == recipient.Id);
        if (recipientClient is null)
        {
            // This should only rarely happen -> critical
            c.Client.Log($"Could not find WorldClient of player {request.Membername}", LogLevel.Critical);
            return; // TODO Send response to sender
        }

        await recipientClient.SendPacket(new SMSG_GROUP_INVITE(c.GetCharacter().Name));
    }

    // CMSG_GROUP_ACCEPT
    // CMSG_GROUP_DECLINE
}
