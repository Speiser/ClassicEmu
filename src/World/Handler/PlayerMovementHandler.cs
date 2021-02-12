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
        public static Task OnPlayerMovePrototype(WorldClient client, byte[] data)
        {
            var request = new MSG_MOVE_GENERIC(data);

            // Always trust the client (for now..)
            client.Character.Position.X = request.MapX;
            client.Character.Position.Y = request.MapY;
            client.Character.Position.Z = request.MapZ;
            client.Character.Position.Orientation = request.MapO;

            return Task.CompletedTask;
        }
    }
}
