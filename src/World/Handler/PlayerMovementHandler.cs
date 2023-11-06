using System.Diagnostics;
using System.Threading.Tasks;
using Classic.World.Packets;
using Classic.World.Packets.Shared;

namespace Classic.World.Handler;

public class PlayerMovementHandler
{
    [OpcodeHandler(Opcode.MSG_MOVE_FALL_LAND)]
    [OpcodeHandler(Opcode.MSG_MOVE_HEARTBEAT)]
    [OpcodeHandler(Opcode.MSG_MOVE_JUMP)]
    [OpcodeHandler(Opcode.MSG_MOVE_SET_FACING)]
    [OpcodeHandler(Opcode.MSG_MOVE_START_BACKWARD)]
    [OpcodeHandler(Opcode.MSG_MOVE_START_FORWARD)]
    [OpcodeHandler(Opcode.MSG_MOVE_START_STRAFE_LEFT)]
    [OpcodeHandler(Opcode.MSG_MOVE_START_STRAFE_RIGHT)]
    [OpcodeHandler(Opcode.MSG_MOVE_START_TURN_LEFT)]
    [OpcodeHandler(Opcode.MSG_MOVE_START_TURN_RIGHT)]
    [OpcodeHandler(Opcode.MSG_MOVE_STOP)]
    [OpcodeHandler(Opcode.MSG_MOVE_STOP_STRAFE)]
    [OpcodeHandler(Opcode.MSG_MOVE_STOP_TURN)]
    public static async Task OnPlayerMovePrototype(PacketHandlerContext c)
    {
        var request = new MSG_MOVE_GENERIC(c.Packet, c.Client.Build);
        var character = await c.World.CharacterService.GetCharacter(c.Client.CharacterId);
        Debug.Assert(character is not null);
        // Always trust the client (for now..)
        character.Position.X = request.MapX;
        character.Position.Y = request.MapY;
        character.Position.Z = request.MapZ;
        character.Position.Orientation = request.MapO;

        if (c.Opcode == Opcode.MSG_MOVE_HEARTBEAT)
        {
            return;
        }

        foreach (var client in c.World.Connections)
        {
            if (!client.IsInWorld) continue;
            if (client.CharacterId == c.Client.CharacterId) continue;

            // Put that in a queue and dont await it?
            _ = client.SendPacket(new MovementUpdate(character.Id, request, c.Opcode, client.Build));
        }
    }
}
