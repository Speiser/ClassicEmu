using System.Threading.Tasks;
using Classic.Shared;
using Classic.World.Messages;
using Classic.World.Messages.Client;
using Classic.World.Messages.Server;

namespace Classic.World.Handler
{
    public class UtilHandler
    {
        [OpcodeHandler(Opcode.CMSG_PING)]
        public static async Task OnPing(PacketHandlerContext c)
        {
            var request = new CMSG_PING(c.Data);
            await c.Client.SendPacket(new SMSG_PONG(request.Latency));
        }

        [OpcodeHandler(Opcode.CMSG_NAME_QUERY)]
        public static async Task OnNameQuery(PacketHandlerContext c)
        {
            var request = new CMSG_NAME_QUERY(c.Data);
            var character = DataStore.CharacterRepository.GetCharacter(request.CharacterID);
            if (character is null)
                return;
            await c.Client.SendPacket(new SMSG_NAME_QUERY_RESPONSE(character, c.Client.Build));
        }

        [OpcodeHandler(Opcode.CMSG_QUERY_TIME)]
        public static async Task OnQueryTime(PacketHandlerContext c) => await c.Client.SendPacket(new SMSG_QUERY_TIME_RESPONSE());

        // Introduced in TBC
        [OpcodeHandler(Opcode.CMSG_REALM_SPLIT)]
        public static async Task OnRealmSplit(PacketHandlerContext c)
        {
            using var reader = new PacketReader(c.Data);
            var decision = reader.ReadUInt32();

            await c.Client.SendPacket(new SMSG_REALM_SPLIT(decision));
        }
    }
}
