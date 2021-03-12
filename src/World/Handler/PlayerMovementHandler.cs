using System.Threading.Tasks;
using Classic.World.Extensions;
using Classic.World.Messages;

namespace Classic.World.Handler
{
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
            var request = new MSG_MOVE_GENERIC(c.Data, c.Client.Build);
            var character = c.GetCharacter();
            // Always trust the client (for now..)
            character.Position.X = request.MapX;
            character.Position.Y = request.MapY;
            character.Position.Z = request.MapZ;
            character.Position.Orientation = request.MapO;

            foreach (var client in c.World.Connections)
            {
                if (!client.IsInWorld) continue;
                if (client.CharacterId == c.Client.CharacterId) continue;

                // Put that in a queue and dont await it?
                await client.SendPacket(new MovementUpdate(character, request, c.Opcode, client.Build));
            }
        }
    }
}
