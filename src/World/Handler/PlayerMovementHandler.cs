using Classic.Common;
using Classic.World.Entities.Enums;

namespace Classic.World.Handler
{
    public class PlayerMovementHandler
    {
        [OpcodeHandler(Opcode.MSG_MOVE_FALL_LAND)]
        [OpcodeHandler(Opcode.MSG_MOVE_HEARTBEAT)]
        [OpcodeHandler(Opcode.MSG_MOVE_JUMP)]
        [OpcodeHandler(Opcode.MSG_MOVE_START_BACKWARD)]
        [OpcodeHandler(Opcode.MSG_MOVE_START_FORWARD)]
        [OpcodeHandler(Opcode.MSG_MOVE_START_STRAFE_LEFT)]
        [OpcodeHandler(Opcode.MSG_MOVE_START_STRAFE_RIGHT)]
        [OpcodeHandler(Opcode.MSG_MOVE_START_TURN_LEFT)]
        [OpcodeHandler(Opcode.MSG_MOVE_START_TURN_RIGHT)]
        [OpcodeHandler(Opcode.MSG_MOVE_STOP)]
        [OpcodeHandler(Opcode.MSG_MOVE_STOP_STRAFE)]
        [OpcodeHandler(Opcode.MSG_MOVE_STOP_TURN)]
        public static void OnPlayerMovePrototype(WorldClient client, byte[] data)
        {
            // Always trust the client (for now..)
            using (var reader = new PacketReader(data))
            {
                var moveFlags = (MovementFlags)reader.ReadUInt32(); // Unhandled
                var time = reader.ReadUInt32(); // Unhandled
                var mapX = reader.ReadFloat();
                var mapY = reader.ReadFloat();
                var mapZ = reader.ReadFloat();
                var mapO = reader.ReadFloat();

                client.Character.Map.X = mapX;
                client.Character.Map.Y = mapY;
                client.Character.Map.Z = mapZ;
                client.Character.Map.Orientation = mapO;
            }
        }
    }
}
