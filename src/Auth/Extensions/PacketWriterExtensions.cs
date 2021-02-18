using Classic.Common;

namespace Classic.Auth.Extensions
{
    internal static class PacketWriterExtensions
    {
        public static PacketWriter WriteNumberOfRealms(this PacketWriter writer, GameVersion gameVersion) => gameVersion switch
        {
            GameVersion.Classic => writer.WriteUInt8(1),
            GameVersion.WotLK => writer.WriteUInt16(1),
            _ => throw new System.NotImplementedException(),
        };
    }
}
