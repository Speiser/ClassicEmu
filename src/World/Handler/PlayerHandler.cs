using System.Linq;
using System.Threading.Tasks;
using Classic.World.Messages.Client;
using Classic.World.Messages.Server;

namespace Classic.World.Handler
{
    public static class PlayerHandler
    {
        [OpcodeHandler(Opcode.CMSG_PLAYER_LOGIN)]
        public static async Task OnPlayerLogin(WorldClient client, byte[] data)
        {
            var request = new CMSG_PLAYER_LOGIN(data);
            var character = client.User.Characters.Single(x => x.ID == request.CharacterID);

            client.Log($"Player logged in with char {character.Name}");

            await client.SendPacket(new SMSG_LOGIN_VERIFY_WORLD(character));
            await client.SendPacket(new SMSG_ACCOUNT_DATA_TIMES());

            await client.SendPacket(new SMSG_MESSAGECHAT(character.ID, "Hello World"));
            await client.SendPacket(new SMSG_MESSAGECHAT(character.ID, "World Hello"));

            await client.SendPacket(new SMSG_SET_REST_START());
            await client.SendPacket(new SMSG_BINDPOINTUPDATE(character));
            await client.SendPacket(new SMSG_TUTORIAL_FLAGS());
            await client.SendPacket(new SMSG_LOGIN_SETTIMESPEED());

            await client.SendPacket(new SMSG_INITIAL_SPELLS(character.Spells));
            await client.SendPacket(new SMSG_ACTION_BUTTONS(character.ActionBar));
            await client.SendPacket(new SMSG_INITIALIZE_FACTIONS());

            // TODO: not working
            // await client.SendPacket(new SMSG_TRIGGER_CINEMATIC(CinematicID.NightElf));

            await client.SendPacket(new SMSG_CORPSE_RECLAIM_DELAY());
            await client.SendPacket(new SMSG_INIT_WORLD_STATES());
            await client.SendPacket(SMSG_UPDATE_OBJECT.CreateOwnPlayerObject(character, out var player));

            foreach (var unit in client.World.CurrentCreatures)
            {
                // TODO: Add range check
                await client.SendPacket(SMSG_UPDATE_OBJECT.CreateUnit(unit));
            }

            foreach (var other in client.World.CurrentPlayers)
            {
                // TODO: Add range check
                if (other.ID == character.ID) continue; // Should not happen?
                await client.SendPacket(SMSG_UPDATE_OBJECT.CreatePlayer(other));
            }

            client.World.CurrentPlayers.Add(character);
            client.Player = player;
        }

        [OpcodeHandler(Opcode.CMSG_LOGOUT_REQUEST)]
        public static async Task OnPlayerLogoutRequested(WorldClient client, byte[] _)
        {
            await client.SendPacket(SMSG_LOGOUT_COMPLETE.Success());
            client.World.CurrentPlayers.Remove(client.Character);
            client.Player = null;
        }
    }
}
