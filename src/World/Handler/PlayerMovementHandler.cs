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
        public static async Task OnPlayerMovePrototype(HandlerArguments args)
        {
            var request = new MSG_MOVE_GENERIC(args.Data, args.Client.Build);

            // Always trust the client (for now..)
            args.Client.Character.Position.X = request.MapX;
            args.Client.Character.Position.Y = request.MapY;
            args.Client.Character.Position.Z = request.MapZ;
            args.Client.Character.Position.Orientation = request.MapO;

            foreach (var client in args.WorldState.Connections)
            {
                if (client.Character is null) continue;
                if (client.Character.ID == args.Client.Character.ID) continue; // Should not happen?

                // Put that in a queue and dont await it?
                await client.SendPacket(new MovementUpdate(args.Client.Character, request, args.Opcode, client.Build));
            }
        }
    }
}
