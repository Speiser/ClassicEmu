using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Classic.Shared;
using Classic.Shared.Data;
using Classic.World.Data;
using Classic.World.Messages;
using Classic.World.Messages.Client;
using Classic.World.Messages.Server;
using Microsoft.Extensions.Logging;

namespace Classic.World.Handler
{
    public class CharacterHandler
    {
        [OpcodeHandler(Opcode.CMSG_CHAR_ENUM)]
        public static async Task OnCharacterEnum(PacketHandlerContext c)
        {
            var account = c.AccountService.GetAccount(c.Client.Identifier);
            var characters = new List<Character>();

            foreach (var id in account.Characters)
            {
                var character = c.World.CharacterService.GetCharacter(id);
                if (character is null)
                {
                    c.Client.Log($"Could not find character with id {id} from player {account.Identifier}.", LogLevel.Warning);
                    continue;
                }

                characters.Add(character);
            }

            ServerMessageBase<Opcode> characterEnum = c.Client.Build switch
            {
                ClientBuild.Vanilla => new SMSG_CHAR_ENUM_VANILLA(characters),
                ClientBuild.TBC => new SMSG_CHAR_ENUM_TBC(characters),
                _ => throw new NotImplementedException($"OnCharacterEnum(build: {c.Client.Build})"),
            };

            await c.Client.SendPacket(characterEnum);
        }

        [OpcodeHandler(Opcode.CMSG_CHAR_CREATE)]
        public static async Task OnCharacterCreate(PacketHandlerContext c)
        {
            byte status;
            var character = CharacterFactory.Create(new CMSG_CHAR_CREATE(c.Data));
            if (!c.World.CharacterService.AddCharacter(character))
            {
                c.Client.Log($"Could not add created character {character.Name} - {character.Id}.", LogLevel.Warning);
                status = c.Client.Build switch
                {
                    ClientBuild.Vanilla => (byte)CharacterHandlerCode_Vanilla.CHAR_CREATE_ERROR,
                    ClientBuild.TBC => (byte)CharacterHandlerCode_TBC.CHAR_CREATE_ERROR,
                    _ => throw new NotImplementedException($"OnCharacterCreate(build: {c.Client.Build})"),
                };
            }
            else
            {
                var account = c.AccountService.GetAccount(c.Client.Identifier);
                account.Characters.Add(character.Id);
                c.AccountService.UpdateAccount(account);

                status = c.Client.Build switch
                {
                    ClientBuild.Vanilla => (byte)CharacterHandlerCode_Vanilla.CHAR_CREATE_SUCCESS,
                    ClientBuild.TBC => (byte)CharacterHandlerCode_TBC.CHAR_CREATE_SUCCESS,
                    _ => throw new NotImplementedException($"OnCharacterCreate(build: {c.Client.Build})"),
                };
            }

            await c.Client.SendPacket(new SMSG_CHAR_CREATE(status));
        }

        [OpcodeHandler(Opcode.CMSG_CHAR_DELETE)]
        public static async Task OnCharacterDelete(PacketHandlerContext c)
        {
            var request = new CMSG_CHAR_DELETE(c.Data);
            var account = c.AccountService.GetAccount(c.Client.Identifier);

            // Removing character of other account
            if (!account.Characters.Contains(request.CharacterId))
            {
                await c.Client.SendPacket(GetFailedPacket(c.Client.Build));
                return;
            }

            if (!c.World.CharacterService.DeleteCharacter(request.CharacterId))
            {
                await c.Client.SendPacket(GetFailedPacket(c.Client.Build));
                return;
            }

            account.Characters.Remove(request.CharacterId);
            c.AccountService.UpdateAccount(account);

            var status = c.Client.Build switch
            {
                ClientBuild.Vanilla => (byte)CharacterHandlerCode_Vanilla.CHAR_DELETE_SUCCESS,
                ClientBuild.TBC => (byte)CharacterHandlerCode_TBC.CHAR_DELETE_SUCCESS,
                _ => throw new NotImplementedException($"OnCharacterDelete(build: {c.Client.Build})"),
            };

            await c.Client.SendPacket(new SMSG_CHAR_DELETE(status));
        }

        private static SMSG_CHAR_DELETE GetFailedPacket(int build)
        {
            var failedStatus = build switch
            {
                ClientBuild.Vanilla => (byte)CharacterHandlerCode_Vanilla.CHAR_DELETE_FAILED,
                ClientBuild.TBC => (byte)CharacterHandlerCode_TBC.CHAR_DELETE_FAILED,
                _ => throw new NotImplementedException($"OnCharacterDelete(build: {build})"),
            };
            return new SMSG_CHAR_DELETE(failedStatus);
        }
    }

    public enum CharacterHandlerCode_Vanilla
    {
        CHAR_CREATE_IN_PROGRESS = 0x2D,
        CHAR_CREATE_SUCCESS = 0x2E,
        CHAR_CREATE_ERROR = 0x2F,
        CHAR_CREATE_FAILED = 0x30,
        CHAR_CREATE_NAME_IN_USE = 0x31,
        CHAR_CREATE_DISABLED = 0x3A,
        CHAR_CREATE_PVP_TEAMS_VIOLATION = 0x33,
        CHAR_CREATE_SERVER_LIMIT = 0x34,
        CHAR_CREATE_ACCOUNT_LIMIT = 0x35,

        CHAR_DELETE_IN_PROGRESS = 0x38,
        CHAR_DELETE_SUCCESS = 0x39,
        CHAR_DELETE_FAILED = 0x3A,
    }

    public enum CharacterHandlerCode_TBC
    {
        CHAR_CREATE_IN_PROGRESS = 0x2E,
        CHAR_CREATE_SUCCESS = 0x2F,
        CHAR_CREATE_ERROR = 0x30,
        CHAR_CREATE_FAILED = 0x31,
        CHAR_CREATE_NAME_IN_USE = 0x32,
        CHAR_CREATE_DISABLED = 0x33,
        CHAR_CREATE_PVP_TEAMS_VIOLATION = 0x34,
        CHAR_CREATE_SERVER_LIMIT = 0x35,
        CHAR_CREATE_ACCOUNT_LIMIT = 0x36,
        CHAR_CREATE_SERVER_QUEUE = 0x37,
        CHAR_CREATE_ONLY_EXISTING = 0x38,
        CHAR_CREATE_EXPANSION = 0x39,

        CHAR_DELETE_IN_PROGRESS = 0x3A,
        CHAR_DELETE_SUCCESS = 0x3B,
        CHAR_DELETE_FAILED = 0x3C,
        CHAR_DELETE_FAILED_LOCKED_FOR_TRANSFER = 0x3D,
        CHAR_DELETE_FAILED_GUILD_LEADER = 0x3E,
        CHAR_DELETE_FAILED_ARENA_CAPTAIN = 0x3F,
    }
}
