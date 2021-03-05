using Classic.Shared;
using Classic.World.Data;

namespace Classic.World.Extensions
{
    internal static class PacketWriterExtensions
    {
        public static PacketWriter WriteMap(this PacketWriter writer, Map map)
        {
            return writer
                .WriteFloat(map.X)
                .WriteFloat(map.Y)
                .WriteFloat(map.Z)
                .WriteFloat(map.Orientation);
        }
    }
}