using System.Linq;
using Classic.Common;
using Classic.World.Messages;

namespace Classic.World.Handler
{
    public static class PlayerHandler
    {
        [OpcodeHandler(Opcode.CMSG_PLAYER_LOGIN)]
        public static void OnPlayerLogin(WorldClient client, byte[] data)
        {
            uint charId = 0;

            using (var reader = new PacketReader(data))
            {
                reader.Skip(6);
                charId = reader.ReadUInt32();
            }

            var character = client.User.Characters.Single(x => x.ID == charId);

            client.Log($"Player logged in with char {character.Name}");

            client.SendPacket(new SMSG_LOGIN_VERIFY_WORLD());
            client.SendPacket(new SMSG_ACCOUNT_DATA_TIMES());

            // TODO: Server messages??

            client.SendPacket(new SMSG_SET_REST_START());
            client.SendPacket(new SMSG_BINDPOINTUPDATE());
            client.SendPacket(new SMSG_TUTORIAL_FLAGS());
            client.SendPacket(new SMSG_LOGIN_SETTIMESPEED());

            // TODO: SMSG_INITIAL_SPELLS
            // TODO: SMSG_ACTION_BUTTONS
            // TODO: SMSG_INITIALIZE_FACTIONS
            // TODO: SMSG_TRIGGER_CINEMATIC

            client.SendPacket(new SMSG_CORPSE_RECLAIM_DELAY());
            client.SendPacket(new SMSG_INIT_WORLD_STATES());
            client.SendPacket(new SMSG_UPDATE_OBJECT(character));

            // Client sends back CMSG_UPDATE_ACCOUNT_DATA
        }
    }
}
