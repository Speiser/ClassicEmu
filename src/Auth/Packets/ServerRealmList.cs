using Classic.Auth.Extensions;
using Classic.Shared;
using Classic.Shared.Data;

namespace Classic.Auth.Packets;

public class ServerRealmlist
{
    public static byte[] Get(Realm[] realms, int build)
    {
        using var info = new PacketWriter()
            .WriteUInt32(/* unk */ 0)
            .WriteNumberOfRealms(realms.Length, build);

        foreach (var realm in realms)
        {
            info.WriteRealmType(realm.Type, build);

            if (build > ClientBuild.Vanilla)
            {
                info.WriteUInt8(realm.Lock); // 1 is lock
            }

            info
                .WriteUInt8((byte)realm.Flags)
                .WriteString(realm.Name)
                .WriteString(realm.Address)
                .WriteUInt32(realm.Population)
                .WriteUInt8( /* num_chars  */ 0)
                .WriteUInt8(realm.TimeZone);

            if (build == ClientBuild.Vanilla)
            {
                info.WriteUInt8( /* unk */ 0);
            }
            else
            {
                // Realm ID for tbc and wotlk
                info.WriteUInt8(0x2C);
            }
        }

        if (build == ClientBuild.Vanilla)
        {
            info.WriteUInt16(0x0002);
        }
        else
        {
            info.WriteUInt16(0x0010);
        }

        var infoPacket = info.Build();

        using var header = new PacketWriter()
            .WriteUInt8((byte)Opcode.Realmlist)
            .WriteUInt16((ushort)infoPacket.Length);

        return header.WriteBytes(infoPacket).Build();
    }
}
