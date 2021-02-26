﻿using System.Linq;
using System.Threading.Tasks;
using Classic.Common;
using Classic.World.Extensions;
using Classic.World.Messages;
using Classic.World.Messages.Client;
using Classic.World.Messages.Server;

namespace Classic.World.Handler
{
    public static class PlayerHandler
    {
        private const string MessageOfTheDay = "https://github.com/Speiser/ClassicEmu";

        [OpcodeHandler(Opcode.CMSG_PLAYER_LOGIN)]
        public static async Task OnPlayerLogin(HandlerArguments args)
        {
            var request = new CMSG_PLAYER_LOGIN(args.Data);
            var character = args.Client.Session.Account.Characters.Single(x => x.ID == request.CharacterID);

            args.Client.Log($"Player logged in with char {character.Name}");

            if (args.IsTBC())
            {
                await args.SendPacket<MSG_SET_DUNGEON_DIFFICULTY>();
            }

            await args.Client.SendPacket(new SMSG_LOGIN_VERIFY_WORLD(character));
            await args.SendPacket<SMSG_ACCOUNT_DATA_TIMES>();

            if (args.IsTBC())
            {
                await args.SendPacket<SMSG_FEATURE_SYSTEM_STATUS>();
            }

            await args.Client.SendPacket(new SMSG_EXPECTED_SPAM_RECORDS(Enumerable.Empty<string>()));

            if (args.IsVanilla())
            {
                await args.Client.SendPacket(new SMSG_MESSAGECHAT(character.ID, MessageOfTheDay));
            }
            else if (args.IsTBC())
            {
                await args.Client.SendPacket(new SMSG_MOTD(MessageOfTheDay));
            }

            // await args.Client.SendPacket(new SMSG_NAME_QUERY_RESPONSE(character, args.Client.Build));
            // if (GUILD) -> SMSG_GUILD_EVENT

            if (character.Stats.Life == 0)
            {
                await args.SendPacket<SMSG_CORPSE_RECLAIM_DELAY>();
            }

            await args.SendPacket<SMSG_SET_REST_START>();
            await args.Client.SendPacket(new SMSG_BINDPOINTUPDATE(character));
            await args.SendPacket<SMSG_TUTORIAL_FLAGS>();

            if (args.IsTBC())
            {
                await args.SendPacket<SMSG_INSTANCE_DIFFICULTY>();
            }

            await args.Client.SendPacket(new SMSG_INITIAL_SPELLS(character.Spells));
            // SMSG_SEND_UNLEARN_SPELLS
            await args.Client.SendPacket(new SMSG_ACTION_BUTTONS(character.ActionBar));
            await args.Client.SendPacket(new SMSG_INITIALIZE_FACTIONS(ClientBuild.Vanilla));
            await args.SendPacket<SMSG_LOGIN_SETTIMESPEED>();
            // await args.Client.SendPacket(new SMSG_TRIGGER_CINEMATIC(CinematicID.NightElf));

            await args.Client.SendPacket(SMSG_UPDATE_OBJECT.CreateOwnPlayerObject(character, args.Client.Build, out var player));
            args.Client.Player = player;

            // TODO: Implement for TBC
            if (args.IsVanilla())
            {
                // Initially spawn all creatures
                foreach (var unit in args.WorldState.Creatures)
                {
                    // TODO: Add range check
                    await args.Client.SendPacket(SMSG_UPDATE_OBJECT_VANILLA.CreateUnit(unit));
                }

                await args.WorldState.SpawnPlayer(character);
            }

            // if (GROUP) -> SMSG_GROUP_LIST
            // if Vanilla
            //     SMSG_FRIEND_LIST
            //     SMSG_IGNORE_LIST
            // if TBC
            //     SMSG_CONTACT_LIST

            if (args.IsTBC())
            {
                await args.SendPacket<SMSG_TIME_SYNC_REQ>();
            }
            
            // SMSG_ITEM_ENCHANT_TIME_UPDATE
            // SMSG_ITEM_TIME_UPDATE
            // SMSG_FRIEND_STATUS
        }

        [OpcodeHandler(Opcode.CMSG_LOGOUT_REQUEST)]
        public static async Task OnPlayerLogoutRequested(HandlerArguments args)
        {
            await args.Client.SendPacket(SMSG_LOGOUT_COMPLETE.Success());
            args.Client.Player = null;

            // TODO: Remove from other clients
        }
    }
}
