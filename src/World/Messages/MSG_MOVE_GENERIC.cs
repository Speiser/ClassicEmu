using Classic.Common;
using Classic.World.Entities.Enums;

namespace Classic.World.Messages
{
    public class MSG_MOVE_GENERIC
    {
        public MSG_MOVE_GENERIC(byte[] data, int build)
        {
            using var reader = new PacketReader(data);
            MovementFlags = (MovementFlags)reader.ReadUInt32();
            if (build == ClientBuild.TBC) reader.ReadByte(); // moveflags2
            Time = reader.ReadUInt32();
            MapX = reader.ReadFloat();
            MapY = reader.ReadFloat();
            MapZ = reader.ReadFloat();
            MapO = reader.ReadFloat();
        }

        public MovementFlags MovementFlags { get; }
        public uint Time { get; }
        public float MapX { get; }
        public float MapY { get; }
        public float MapZ { get; }
        public float MapO { get; }
    }
}
