using Classic.Shared;
using Classic.Shared.Data;
using Classic.Shared.Data.Enums;

namespace Classic.Auth.Extensions;

internal static class PacketWriterExtensions
{
    public static PacketWriter WriteNumberOfRealms(this PacketWriter writer, int numberOfRealms, int build) => build switch
    {
        ClientBuild.Vanilla => writer.WriteUInt8((byte)numberOfRealms),
        ClientBuild.TBC or ClientBuild.WotLK => writer.WriteUInt16((ushort)numberOfRealms),
        _ => throw new System.NotImplementedException(),
    };

    public static PacketWriter WriteRealmType(this PacketWriter writer, RealmType realmType, int build) => build switch
    {
        ClientBuild.Vanilla => writer.WriteUInt32((uint)realmType),
        ClientBuild.TBC or ClientBuild.WotLK => writer.WriteUInt8((byte)realmType),
        _ => throw new System.NotImplementedException(),
    };
}
