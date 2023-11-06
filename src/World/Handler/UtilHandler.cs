using System.Threading.Tasks;
using Classic.Shared;
using Classic.World.Packets;
using Classic.World.Packets.Client;
using Classic.World.Packets.Server;

namespace Classic.World.Handler;

public class UtilHandler
{
    [OpcodeHandler(Opcode.CMSG_PING)]
    public static async Task OnPing(PacketHandlerContext c)
    {
        var request = new CMSG_PING(c.Packet);
        await c.Client.SendPacket(new SMSG_PONG(request.Latency));
    }

    [OpcodeHandler(Opcode.CMSG_NAME_QUERY)]
    public static async Task OnNameQuery(PacketHandlerContext c)
    {
        var request = new CMSG_NAME_QUERY(c.Packet);
        var character = await c.World.CharacterService.GetCharacter(request.CharacterID);
        if (character is null)
            return;
        await c.Client.SendPacket(new SMSG_NAME_QUERY_RESPONSE(character, c.Client.Build));
    }

    [OpcodeHandler(Opcode.CMSG_QUERY_TIME)]
    public static async Task OnQueryTime(PacketHandlerContext c) => await c.Client.SendPacket(new SMSG_QUERY_TIME_RESPONSE());

    // Introduced in TBC
    [OpcodeHandler(Opcode.CMSG_REALM_SPLIT)]
    public static async Task OnRealmSplit(PacketHandlerContext c)
    {
        using var reader = new PacketReader(c.Packet);
        var decision = reader.ReadUInt32();

        await c.Client.SendPacket(new SMSG_REALM_SPLIT(decision));
    }
}
