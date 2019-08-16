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
                charId = reader.ReadUInt32();
            }

            var character = client.User.Characters.Single(x => x.ID == charId);

            client.Log($"Player logged in with char {character.Name}");

            client.SendPacket(new SMSG_LOGIN_VERIFY_WORLD());
            client.SendPacket(new SMSG_ACCOUNT_DATA_TIMES());

            client.SendPacket(new SMSG_MESSAGECHAT(character.ID, "Hello World"));
            client.SendPacket(new SMSG_MESSAGECHAT(character.ID, "World Hello"));

            client.SendPacket(new SMSG_SET_REST_START());
            client.SendPacket(new SMSG_BINDPOINTUPDATE());
            client.SendPacket(new SMSG_TUTORIAL_FLAGS());
            client.SendPacket(new SMSG_LOGIN_SETTIMESPEED());

            client.SendPacket(new SMSG_INITIAL_SPELLS());
            client.SendPacket(new SMSG_ACTION_BUTTONS());
            client.SendPacket(new SMSG_INITIALIZE_FACTIONS());
            // TODO: SMSG_TRIGGER_CINEMATIC (Human_ID = 81??)

            client.SendPacket(new SMSG_CORPSE_RECLAIM_DELAY());
            client.SendPacket(new SMSG_INIT_WORLD_STATES());
            client.SendPacket(SMSG_UPDATE_OBJECT.CreateOwnPlayerObject(character, out var player));

            client.OnPlayerSpawn(player);

            client.SendPacket(new SMSG_MESSAGECHAT(character.ID, "Hello World"));
            client.SendPacket(new SMSG_MESSAGECHAT(character.ID, "World Hello"));
        }

        [OpcodeHandler(Opcode.CMSG_LOGOUT_REQUEST)]
        public static void OnPlayerLogoutRequested(WorldClient client, byte[] data)
        {
            client.SendPacket(SMSG_LOGOUT_COMPLETE.Success());
            client.OnPlayerLogout();
        }
    }
}
