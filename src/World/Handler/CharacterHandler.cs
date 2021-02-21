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
                ClientBuild.Vanilla => new SMSG_CHAR_ENUM_VANILLA(args.Client.User.Characters),
                ClientBuild.TBC => new SMSG_CHAR_ENUM_TBC(args.Client.User.Characters),
                _ => throw new NotImplementedException($"OnCharacterEnum(build: {args.Client.Build})"),
            };

            await args.Client.SendPacket(characterEnum);
        }

        [OpcodeHandler(Opcode.CMSG_CHAR_CREATE)]
        public static async Task OnCharacterCreate(HandlerArguments args)
        {
            var character = CMSG_CHAR_CREATE.RequestAsCharacter(args.Data);
            args.Client.User.Characters.Add(character);

            await args.Client.SendPacket(SMSG_CHAR_CREATE.Success());
        }

        [OpcodeHandler(Opcode.CMSG_CHAR_DELETE)]
        public static async Task OnCharacterDelete(HandlerArguments args)
        {
            var request = new CMSG_CHAR_DELETE(args.Data);

            var toBeDeleted = args.Client.User.Characters.Where(c => c.ID == request.CharacterID).SingleOrDefault();

            if (toBeDeleted is null)
            {
                await args.Client.SendPacket(SMSG_CHAR_DELETE.Fail());
                return;
            }

            await args.Client.SendPacket(args.Client.User.Characters.TryTake(out toBeDeleted)
                ? SMSG_CHAR_DELETE.Success()
                : SMSG_CHAR_DELETE.Fail());
        }
    }
}
