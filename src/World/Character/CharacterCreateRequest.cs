using System;
using System.Collections.Generic;
using System.Text;
using Classic.Common;
using Classic.Data.CharacterEnums;
using DCharacter = Classic.Data.Character;

namespace Classic.World.Character
{
    public class CharacterCreateRequest
    {
        public DCharacter Read(byte[] packet)
        {
            using (var reader = new PacketReader(packet))
            {
                // TODO: Maybe evaluate later: len, cmd ??
                reader.Skip(6);

                var character = new DCharacter
                {
                    Name = reader.ReadString(),
                    Race = reader.ReadByte().AsEnum<Races>(),
                    Class = reader.ReadByte().AsEnum<Classes>(),
                    Gender = reader.ReadByte().AsEnum<Genders>(),
                    Skin = reader.ReadByte(),
                    Face = reader.ReadByte(),
                    HairStyle = reader.ReadByte(),
                    HairColor = reader.ReadByte(),
                    FacialHair = reader.ReadByte(),
                    OutfitId = reader.ReadByte()
                };

                return character;
            }
        }
    }
}
