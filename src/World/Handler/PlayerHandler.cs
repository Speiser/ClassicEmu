using System;
using System.Linq;
using System.Threading.Tasks;
using Classic.Common;
using Classic.Data;
using Classic.World.Extensions;
using Classic.World.Messages;
using Classic.World.Messages.Client;
using Classic.World.Messages.Server;

namespace Classic.World.Handler
{
    public static class PlayerHandler
    {
        [OpcodeHandler(Opcode.CMSG_PLAYER_LOGIN)]
        public static async Task OnPlayerLogin(HandlerArguments args)
        {
            var request = new CMSG_PLAYER_LOGIN(args.Data);
            var character = args.Client.User.Characters.Single(x => x.ID == request.CharacterID);

            args.Client.Log($"Player logged in with char {character.Name}");

            if (args.IsVanilla()) await OnPlayerLoginVanilla(args, character);
            else if (args.IsTBC()) await OnPlayerLoginTBC(args, character);
            else throw new NotImplementedException($"SMSG_UPDATE_OBJECT(build: {args.Client.Build})");
        }

        [OpcodeHandler(Opcode.CMSG_LOGOUT_REQUEST)]
        public static async Task OnPlayerLogoutRequested(HandlerArguments args)
        {
            await args.Client.SendPacket(SMSG_LOGOUT_COMPLETE.Success());
            args.Client.Player = null;

            // TODO: Remove from other clients
        }

        private static async Task OnPlayerLoginVanilla(HandlerArguments args, Character character)
        {
            await args.Client.SendPacket(new SMSG_LOGIN_VERIFY_WORLD(character));
            await args.SendPacket<SMSG_ACCOUNT_DATA_TIMES>();
            //SMSG_EXPECTED_SPAM_RECORDS
            await args.Client.SendPacket(new SMSG_MESSAGECHAT(character.ID, "https://github.com/Speiser/ClassicEmu"));
            //await args.Client.SendPacket(new SMSG_NAME_QUERY_RESPONSE(character, args.Client.Build));
            //if (GUILD) -> SMSG_GUILD_EVENT
            if (character.Stats.Life == 0)
            {
                await args.SendPacket<SMSG_CORPSE_RECLAIM_DELAY>();
            }
            await args.SendPacket<SMSG_SET_REST_START>();
            await args.Client.SendPacket(new SMSG_BINDPOINTUPDATE(character));
            await args.SendPacket<SMSG_TUTORIAL_FLAGS>();
            await args.Client.SendPacket(new SMSG_INITIAL_SPELLS(character.Spells));
            //SMSG_SEND_UNLEARN_SPELLS
            await args.Client.SendPacket(new SMSG_ACTION_BUTTONS(character.ActionBar));
            await args.Client.SendPacket(new SMSG_INITIALIZE_FACTIONS(ClientBuild.Vanilla));
            await args.SendPacket<SMSG_LOGIN_SETTIMESPEED>();
            // await args.Client.SendPacket(new SMSG_TRIGGER_CINEMATIC(CinematicID.NightElf));

            await args.Client.SendPacket(SMSG_UPDATE_OBJECT_VANILLA.CreateOwnPlayerObject(character, out var player));

            // Initially spawn all creatures
            foreach (var unit in args.WorldState.Creatures)
            {
                // TODO: Add range check
                await args.Client.SendPacket(SMSG_UPDATE_OBJECT_VANILLA.CreateUnit(unit));
            }

            args.Client.Player = player;
            await args.WorldState.SpawnPlayer(character);

            //if (GROUP) ->SMSG_GROUP_LIST
            //SMSG_FRIEND_LIST
            //SMSG_IGNORE_LIST
            //SMSG_ITEM_ENCHANT_TIME_UPDATE
            //SMSG_ITEM_TIME_UPDATE
            //SMSG_FRIEND_STATUS
        }

        private static async Task OnPlayerLoginTBC(HandlerArguments args, Character character)
        {
            await args.SendPacket<MSG_SET_DUNGEON_DIFFICULTY>();
            await args.Client.SendPacket(new SMSG_LOGIN_VERIFY_WORLD(character));
            await args.SendPacket<SMSG_ACCOUNT_DATA_TIMES>();
            await args.SendPacket<SMSG_FEATURE_SYSTEM_STATUS>();
            //SMSG_EXPECTED_SPAM_RECORDS(WorldSession::SendExpectedSpamRecords)
            await args.Client.SendPacket(new SMSG_MESSAGECHAT(character.ID, "https://github.com/Speiser/ClassicEmu")); // TODO Use SMSG_MOTD
            //await args.Client.SendPacket(new SMSG_NAME_QUERY_RESPONSE(character, args.Client.Build));
            //if (GUILD) -> SMSG_GUILD_EVENT(WorldSession::HandlePlayerLogin)
            if (character.Stats.Life == 0)
            {
                await args.SendPacket<SMSG_CORPSE_RECLAIM_DELAY>();
            }
            await args.SendPacket<SMSG_SET_REST_START>();
            await args.Client.SendPacket(new SMSG_BINDPOINTUPDATE(character));
            await args.SendPacket<SMSG_TUTORIAL_FLAGS>();
            await args.SendPacket<SMSG_INSTANCE_DIFFICULTY>();
            await args.Client.SendPacket(new SMSG_INITIAL_SPELLS(character.Spells));
            //SMSG_SEND_UNLEARN_SPELLS(Player::SendUnlearnSpells)
            await args.Client.SendPacket(new SMSG_ACTION_BUTTONS(character.ActionBar));
            await args.Client.SendPacket(new SMSG_INITIALIZE_FACTIONS(ClientBuild.TBC));
            await args.SendPacket<SMSG_LOGIN_SETTIMESPEED>();
            // await args.Client.SendPacket(new SMSG_TRIGGER_CINEMATIC(CinematicID.NightElf));

            await args.Client.SendPacket(SMSG_UPDATE_OBJECT_TBC.CreateOwnPlayerObject(character, out var player));

            //if (GROUP) -> SMSG_GROUP_LIST(Group::SendUpdateTo)
            //- "NEW" - SMSG_CONTACT_LIST(PlayerSocial::SendSocialList)
            await args.SendPacket<SMSG_TIME_SYNC_REQ>();
            //SMSG_ITEM_ENCHANT_TIME_UPDATE(WorldSession::SendItemEnchantTimeUpdate)
            //SMSG_ITEM_TIME_UPDATE(Item::SendTimeUpdate)
            //SMSG_FRIEND_STATUS(SocialMgr::MakeFriendStatusPacket)

            //args.Client.Player = player;
        }
    }
}
