using Classic.Common;
using static Classic.Auth.Opcode;

namespace Classic.Auth.Challenges
{
    public class ServerRealmList
    {
        public static void Send(ClientBase client)
        {
            var info = new PacketWriter()
                .WriteUInt32(/* type       */ 1)
                .WriteUInt8( /* flags      */ 0)
                .WriteString(/* name       */ "TestServer")
                .WriteString(/* addr_port  */ "127.0.0.1:13250")
                .WriteUInt32(/* population */ 0)
                .WriteUInt8( /* num_chars  */ 0)
                .WriteUInt8( /* time_zone  */ 0)
                .WriteUInt8( /* unk        */ 0);

            var infoPacket = info.Build();

            var footer = new PacketWriter().WriteUInt16(0);

            var header = new PacketWriter()
                .WriteUInt8( /* cmd        */ (byte)REALMLIST)
                .WriteUInt16(/* size       */ (ushort)(infoPacket.Length + 7))
                .WriteUInt32(/* unk        */ 0)
                .WriteUInt8( /* num_realms */ 1);

            client.Send(header.Build());
            client.Send(infoPacket);
            client.Send(footer.Build());

            header.Dispose();
            info.Dispose();
            footer.Dispose();
        }
    }
}
