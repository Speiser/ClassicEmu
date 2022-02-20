using System.Threading.Tasks;
using Classic.World.Data;
using Classic.World.Extensions;
using Classic.World.Packets;
using Classic.World.Packets.Client;
using Classic.World.Packets.Server;

namespace Classic.World.Handler;

public class ChatHandler
{
    [OpcodeHandler(Opcode.CMSG_MESSAGECHAT)]
    public static async Task OnMessageChat(PacketHandlerContext c)
    {
        var request = new CMSG_MESSAGECHAT(c.Packet);

        // debugging stuff :D
        if (request.Message == ".update")
        {
            if (c.IsVanilla())
            {
                await c.Client.SendPacket(SMSG_UPDATE_OBJECT_VANILLA.UpdateValues(c.World.Creatures));
            }
            return;
        }
        if (request.Message.StartsWith(".spawn"))
        {
            var spawnId = int.Parse(request.Message.Split(" ")[1]);
            var creature = new Creature { Model = spawnId, Position = c.GetCharacter().Position.Copy() };
            await c.World.SpawnCreature(creature);
        }
    }
}
