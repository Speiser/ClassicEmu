using System;
using System.Linq;
using System.Threading.Tasks;
using Classic.Common;
using Classic.World.Messages.Client;
using Classic.World.Messages.Server;

namespace Classic.World.Handler
{
    public class CharacterHandler
    {
        [OpcodeHandler(Opcode.CMSG_CHAR_ENUM)]
        public static async Task OnCharacterEnum(HandlerArguments args)
        {
            ServerMessageBase<Opcode> characterEnum = args.Client.Build switch
            {
                ClientBuild.Vanilla => new SMSG_CHAR_ENUM_VANILLA(args.Client.Session.Account.Characters),
                ClientBuild.TBC => new SMSG_CHAR_ENUM_TBC(args.Client.Session.Account.Characters),
                _ => throw new NotImplementedException($"OnCharacterEnum(build: {args.Client.Build})"),
            };

            await args.Client.SendPacket(characterEnum);
        }

        [OpcodeHandler(Opcode.CMSG_CHAR_CREATE)]
        public static async Task OnCharacterCreate(HandlerArguments args)
        {
            var character = CharacterFactory.Create(new CMSG_CHAR_CREATE(args.Data));
            args.Client.Session.Account.Characters.Add(character);

            var status = args.Client.Build switch
            {
                ClientBuild.Vanilla => (byte)CharacterHandlerCode_Vanilla.CHAR_CREATE_SUCCESS,
                ClientBuild.TBC => (byte)CharacterHandlerCode_TBC.CHAR_CREATE_SUCCESS,
                _ => throw new NotImplementedException($"OnCharacterCreate(build: {args.Client.Build})"),
            };

            await args.Client.SendPacket(new SMSG_CHAR_CREATE(status));
        }

        [OpcodeHandler(Opcode.CMSG_CHAR_DELETE)]
        public static async Task OnCharacterDelete(HandlerArguments args)
        {
            var request = new CMSG_CHAR_DELETE(args.Data);

            var toBeDeleted = args.Client.Session.Account.Characters.Where(c => c.ID == request.CharacterID).SingleOrDefault();

            if (toBeDeleted is null || !args.Client.Session.Account.Characters.TryTake(out toBeDeleted))
            {
                var failedStatus = args.Client.Build switch
                {
                    ClientBuild.Vanilla => (byte)CharacterHandlerCode_Vanilla.CHAR_DELETE_FAILED,
                    ClientBuild.TBC => (byte)CharacterHandlerCode_TBC.CHAR_DELETE_FAILED,
                    _ => throw new NotImplementedException($"OnCharacterDelete(build: {args.Client.Build})"),
                };
                await args.Client.SendPacket(new SMSG_CHAR_DELETE(failedStatus));
                return;
            }

            var status = args.Client.Build switch
            {
                ClientBuild.Vanilla => (byte)CharacterHandlerCode_Vanilla.CHAR_DELETE_SUCCESS,
                ClientBuild.TBC => (byte)CharacterHandlerCode_TBC.CHAR_DELETE_SUCCESS,
                _ => throw new NotImplementedException($"OnCharacterDelete(build: {args.Client.Build})"),
            };

            await args.Client.SendPacket(new SMSG_CHAR_DELETE(status));
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
