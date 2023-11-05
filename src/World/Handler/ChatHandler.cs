using System.Diagnostics;
using System.Threading.Tasks;
using Classic.World.Data;
using Classic.World.Packets;
using Classic.World.Packets.Client;

namespace Classic.World.Handler;

public class ChatHandler
{
    [OpcodeHandler(Opcode.CMSG_MESSAGECHAT)]
    public static async Task OnMessageChat(PacketHandlerContext c)
    {
        var request = new CMSG_MESSAGECHAT(c.Packet);

        if (request.Message.StartsWith(".spawn"))
        {
            var spawnId = int.Parse(request.Message.Split(" ")[1]);
            var character = await c.World.CharacterService.GetCharacter(c.Client.CharacterId);
            Debug.Assert(character is not null);
            var creature = new Creature { Model = spawnId, Position = character.Position.Copy() };
            await c.World.SpawnCreature(creature);
        }
    }
}
