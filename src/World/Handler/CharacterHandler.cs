using Classic.Common;
using Classic.Data;
using Classic.Data.Enums.Character;
using Classic.World.Messages;
using System.Linq;

namespace Classic.World.Handler
{
    public class CharacterHandler
    {
        [OpcodeHandler(Opcode.CMSG_CHAR_ENUM)]
        public static void OnCharacterEnum(WorldClient client, byte[] _)
        {
            client.SendPacket(new SMSG_CHAR_ENUM(client.User.Characters));
        }

        [OpcodeHandler(Opcode.CMSG_CHAR_CREATE)]
        public static void OnCharacterCreate(WorldClient client, byte[] data)
        {
            using (var reader = new PacketReader(data))
            {
                var character = new Character
                {
                    Name = reader.ReadString(),
                    Race = reader.ReadByte().AsEnum<Race>(),
                    Class = reader.ReadByte().AsEnum<Classes>(),
                    Gender = reader.ReadByte().AsEnum<Gender>(),
                    Skin = reader.ReadByte(),
                    Face = reader.ReadByte(),
                    HairStyle = reader.ReadByte(),
                    HairColor = reader.ReadByte(),
                    FacialHair = reader.ReadByte(),
                    OutfitId = reader.ReadByte(),
                    Level = 1,
                };

                character.Position = Map.StartingAreas[character.Race];
                client.User.Characters.Add(character);
            }

            client.SendPacket(new SMSG_CHAR_CREATE());
        }

        [OpcodeHandler(Opcode.CMSG_CHAR_DELETE)]
        public static void OnCharacterDelete(WorldClient client, byte[] data)
        {
            using (var reader = new PacketReader(data))
            {
                var id = reader.ReadUInt64();
                var toBeDeleted = client.User.Characters.Where(c => c.ID == id).Single();
                client.SendPacket(client.User.Characters.TryTake(out toBeDeleted)
                    ? SMSG_CHAR_DELETE.Success()
                    : SMSG_CHAR_DELETE.Fail());
            }
        }
    }
}
