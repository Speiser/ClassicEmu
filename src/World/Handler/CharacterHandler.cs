using System.Linq;
using System.Threading.Tasks;
using Classic.World.Messages.Client;
using Classic.World.Messages.Server;

namespace Classic.World.Handler
{
    public class CharacterHandler
    {
        [OpcodeHandler(Opcode.CMSG_CHAR_ENUM)]
        public static async Task OnCharacterEnum(WorldClient client, byte[] _) => await client.SendPacket(new SMSG_CHAR_ENUM(client.User.Characters));

        [OpcodeHandler(Opcode.CMSG_CHAR_CREATE)]
        public static async Task OnCharacterCreate(WorldClient client, byte[] data)
        {
            var character = CMSG_CHAR_CREATE.RequestAsCharacter(data);
            client.User.Characters.Add(character);

            await client.SendPacket(SMSG_CHAR_CREATE.Success());
        }

        [OpcodeHandler(Opcode.CMSG_CHAR_DELETE)]
        public static async Task OnCharacterDelete(WorldClient client, byte[] data)
        {
            var request = new CMSG_CHAR_DELETE(data);

            var toBeDeleted = client.User.Characters.Where(c => c.ID == request.CharacterID).SingleOrDefault();

            if (toBeDeleted is null)
            {
                await client.SendPacket(SMSG_CHAR_DELETE.Fail());
                return;
            }

            await client.SendPacket(client.User.Characters.TryTake(out toBeDeleted)
                ? SMSG_CHAR_DELETE.Success()
                : SMSG_CHAR_DELETE.Fail());
        }
    }
}
