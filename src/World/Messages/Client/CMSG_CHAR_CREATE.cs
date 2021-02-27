using Classic.Common;
using Classic.Data;
using Classic.Data.Enums.Character;
using Classic.World.Extensions;

namespace Classic.World.Messages.Client
{
    public class CMSG_CHAR_CREATE
    {
        public CMSG_CHAR_CREATE(byte[] data)
        {
            using (var reader = new PacketReader(data))
            {
                Name = reader.ReadString();
                Race = reader.ReadByte().AsEnum<Race>();
                Class = reader.ReadByte().AsEnum<Classes>();
                Gender = reader.ReadByte().AsEnum<Gender>();
                Skin = reader.ReadByte();
                Face = reader.ReadByte();
                HairStyle = reader.ReadByte();
                HairColor = reader.ReadByte();
                FacialHair = reader.ReadByte();
                OutfitId = reader.ReadByte();
            }
        }

        public string Name { get; }
        public Race Race { get; }
        public Classes Class { get; }
        public Gender Gender { get; }
        public byte Skin { get; }
        public byte Face { get; }
        public byte HairStyle { get; }
        public byte HairColor { get; }
        public byte FacialHair { get; }
        public byte OutfitId { get; }

        public static Character RequestAsCharacter(byte[] data)
        {
            var request = new CMSG_CHAR_CREATE(data);

            var character = new Character
            {
                Name = request.Name,
                Race = request.Race,
                Class = request.Class,
                Gender = request.Gender,
                Skin = request.Skin,
                Face = request.Face,
                HairStyle = request.HairStyle,
                HairColor = request.HairColor,
                FacialHair = request.FacialHair,
                OutfitId = request.OutfitId,
                Level = 1,
            };

            character.Position = Map.GetStartingPosition(character.Race);
            return character;
        }
    }
}
