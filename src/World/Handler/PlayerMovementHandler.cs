using System.Threading.Tasks;
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

            // Always trust the client (for now..)
            c.Client.Character.Position.X = request.MapX;
            c.Client.Character.Position.Y = request.MapY;
            c.Client.Character.Position.Z = request.MapZ;
            c.Client.Character.Position.Orientation = request.MapO;

            foreach (var client in c.World.Connections)
            {
                if (client.Character is null) continue;
                if (client.Character.Id == c.Client.Character.Id) continue; // Should not happen?

                // Put that in a queue and dont await it?
                await client.SendPacket(new MovementUpdate(c.Client.Character, request, c.Opcode, client.Build));
            }
        }
    }
}
