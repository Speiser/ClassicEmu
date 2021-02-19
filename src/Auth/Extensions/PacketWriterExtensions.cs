using Classic.Auth.Entities;
using Classic.Common;

namespace Classic.Auth.Extensions
{
    internal static class PacketWriterExtensions
    {
        public static PacketWriter WriteNumberOfRealms(this PacketWriter writer, int numberOfRealms, GameVersion gameVersion) => gameVersion switch
        {
            GameVersion.Classic => writer.WriteUInt8((byte)numberOfRealms),
            GameVersion.WotLK => writer.WriteUInt16((ushort)numberOfRealms),
            _ => throw new System.NotImplementedException(),
        };

        public static PacketWriter WriteRealmType(this PacketWriter writer, int realmType, GameVersion gameVersion) => gameVersion switch
        {
            GameVersion.Classic => writer.WriteUInt32((uint)realmType),
            GameVersion.WotLK => writer.WriteUInt8((byte)realmType),
            _ => throw new System.NotImplementedException(),
        };
    }
}