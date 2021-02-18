using System.Threading.Tasks;
using Classic.Auth.Extensions;
using Classic.Common;
using static Classic.Auth.Opcode;

namespace Classic.Auth.Challenges
{
    public class ServerRealmList
    {
        public static async Task Send(LoginClient client)
        {
            using var info = new PacketWriter()
                .WriteUInt32(/* unk */ 0)
                .WriteNumberOfRealms(client.GameVersion)

                // Put in foreach? Wotlk prolems are somewhere in the rest of this info packet vvvv
                .WriteUInt32(/* type       */ 1);

            if (client.GameVersion == GameVersion.WotLK)
            {
                info.WriteUInt8(/* lock */ 0); // 1 is lock
            }

            info
                .WriteUInt8( /* flags      */ 0)
                .WriteString(/* name       */ "TestServer")
                .WriteString(/* addr_port  */ "127.0.0.1:13250")
                .WriteUInt32(/* population */ 0)
                .WriteUInt8( /* num_chars  */ 0)
                .WriteUInt8( /* time_zone  */ 0);

            if (client.GameVersion == GameVersion.Classic)
            {
                info.WriteUInt8( /* unk        */ 0);
            }
            else if (client.GameVersion == GameVersion.WotLK)
            {
                // Realm ID for tbc and wotlk
                info.WriteUInt8(0x2C);
            }

            var infoPacket = info.Build();

            using var footer = new PacketWriter();

            if (client.GameVersion == GameVersion.Classic)
            {
                footer.WriteUInt8(0x00).WriteUInt8(0x02);
            }
            else if (client.GameVersion == GameVersion.WotLK)
            {
                footer.WriteUInt8(0x10).WriteUInt8(0x00);
            }

            using var header = new PacketWriter()
                .WriteUInt8( /* cmd  */ (byte)REALMLIST)
                .WriteUInt16(/* size */ (ushort)(infoPacket.Length + 2));

            await client.Send(header.WriteBytes(infoPacket).WriteBytes(footer.Build()).Build());
        }
    }


}
