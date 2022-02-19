using Classic.Shared;
using Classic.Shared.Data;
using Classic.World.Entities.Enums;

namespace Classic.World.Packets.Shared;

public class MSG_MOVE_GENERIC
{
    public MSG_MOVE_GENERIC(byte[] data, int build)
    {
        using var reader = new PacketReader(data);
        MovementFlags = (MovementFlags)reader.ReadUInt32();
        if (build == ClientBuild.TBC)
        {
            this.MovementFlags2 = reader.ReadByte(); 
        }
        Time = reader.ReadUInt32();
        MapX = reader.ReadFloat();
        MapY = reader.ReadFloat();
        MapZ = reader.ReadFloat();
        MapO = reader.ReadFloat();
    }

    public MovementFlags MovementFlags { get; }
    public byte MovementFlags2 { get; } // Introduced in TBC
    public uint Time { get; }
    public float MapX { get; }
    public float MapY { get; }
    public float MapZ { get; }
    public float MapO { get; }
}
