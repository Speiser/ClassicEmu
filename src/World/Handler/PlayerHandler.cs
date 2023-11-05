using System.Linq;
using System.Threading.Tasks;
using Classic.Shared.Data;
using Classic.World.Extensions;
using Classic.World.Packets;
using Classic.World.Packets.Client;
using Classic.World.Packets.Server;
using Classic.World.Packets.Shared;
using Microsoft.Extensions.Logging;

namespace Classic.World.Handler;

public static class PlayerHandler
{
    private const string MessageOfTheDay = "https://github.com/Speiser/ClassicEmu";

    [OpcodeHandler(Opcode.CMSG_PLAYER_LOGIN)]
    public static async Task OnPlayerLogin(PacketHandlerContext c)
    {
        var request = new CMSG_PLAYER_LOGIN(c.Packet);
        var character = await c.World.CharacterService.GetCharacter(request.CharacterID);

        // Login with a deleted character or a character from another account.
        // TODO: Get account and check accountId != character.AccountId
        if (character is null)
        {
            c.Client.Log(
                $"{c.Client.Identifier} tried to login with a deleted character or a character from another account.",
                LogLevel.Warning);
            return;
        }

        c.Client.Log($"Player logged in with char {character.Name}");

        if (c.IsTBC())
        {
            await c.SendPacket<MSG_SET_DUNGEON_DIFFICULTY>();
        }

        await c.Client.SendPacket(new SMSG_LOGIN_VERIFY_WORLD(character));
        await c.SendPacket<SMSG_ACCOUNT_DATA_TIMES>();

        if (c.IsTBC())
        {
            await c.SendPacket<SMSG_FEATURE_SYSTEM_STATUS>();
        }

        await c.Client.SendPacket(new SMSG_EXPECTED_SPAM_RECORDS(Enumerable.Empty<string>()));

        if (c.IsVanilla())
        {
            await c.Client.SendPacket(new SMSG_MESSAGECHAT(character.Id, MessageOfTheDay));
        }
        else if (c.IsTBC())
        {
            await c.Client.SendPacket(new SMSG_MOTD(MessageOfTheDay));
        }

        // await args.Client.SendPacket(new SMSG_NAME_QUERY_RESPONSE(character, args.Client.Build));
        // if (GUILD) -> SMSG_GUILD_EVENT

        if (character.Stats.Life == 0)
        {
            await c.SendPacket<SMSG_CORPSE_RECLAIM_DELAY>();
        }

        await c.SendPacket<SMSG_SET_REST_START>();
        await c.Client.SendPacket(new SMSG_BINDPOINTUPDATE(character));
        await c.SendPacket<SMSG_TUTORIAL_FLAGS>();

        if (c.IsTBC())
        {
            await c.SendPacket<SMSG_INSTANCE_DIFFICULTY>();
        }

        await c.Client.SendPacket(new SMSG_INITIAL_SPELLS(character.Spells));
        // SMSG_SEND_UNLEARN_SPELLS
        await c.Client.SendPacket(new SMSG_ACTION_BUTTONS(character.ActionBar));
        await c.Client.SendPacket(new SMSG_INITIALIZE_FACTIONS(ClientBuild.Vanilla)); // BUG??
        await c.SendPacket<SMSG_LOGIN_SETTIMESPEED>();
        // await args.Client.SendPacket(new SMSG_TRIGGER_CINEMATIC(CinematicID.NightElf));

        c.Client.CharacterId = character.Id;
        await c.Client.SendPacket(SMSG_UPDATE_OBJECT.CreateOwnPlayerObject(character, c.Client.Build, out var player));
        c.Client.Player = player;

        // TODO: Implement for TBC
        if (c.IsVanilla())
        {
            // Initially spawn all creatures
            foreach (var unit in c.World.Creatures)
            {
                // TODO: Add range check
                await c.Client.SendPacket(SMSG_UPDATE_OBJECT_VANILLA.CreateUnit(unit));
            }

        }

        await c.World.SpawnPlayer(character, c.Client.Build);

        // if (GROUP) -> SMSG_GROUP_LIST
        // if Vanilla
        //     SMSG_FRIEND_LIST
        //     SMSG_IGNORE_LIST
        // if TBC
        //     SMSG_CONTACT_LIST

        if (c.IsTBC())
        {
            await c.SendPacket<SMSG_TIME_SYNC_REQ>();
        }

        // SMSG_ITEM_ENCHANT_TIME_UPDATE
        // SMSG_ITEM_TIME_UPDATE
        // SMSG_FRIEND_STATUS
    }

    [OpcodeHandler(Opcode.CMSG_LOGOUT_REQUEST)]
    public static async Task OnPlayerLogoutRequested(PacketHandlerContext c)
    {
        await c.Client.SendPacket(SMSG_LOGOUT_COMPLETE.Success());
        c.Client.CharacterId = default;
        c.Client.Player = null;

        // TODO: Remove from other clients
    }
}
