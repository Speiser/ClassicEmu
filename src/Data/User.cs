using System.Collections.Concurrent;
using Classic.Cryptography;
using Classic.Data.CharacterEnums;

namespace Classic.Data
{
    public class User
    {
        private readonly SecureRemotePasswordProtocol srp;
        public User(SecureRemotePasswordProtocol srp)
        {
            this.srp = srp;
            this.Characters = new ConcurrentBag<Character>
            {
                new Character
                {
                    Class = Classes.Warrior,
                    Face = 1,
                    FacialHair = 1,
                    Gender = Gender.Male,
                    HairColor = 1,
                    HairStyle = 1,
                    Level = 1,
                    Map = Map.Default,
                    Name = "Player",
                    OutfitId = 1,
                    Race = Race.Human,
                    Skin = 1
                }
            };
        }

        public string Identifier => this.srp.I;
        public byte[] SessionKey => this.srp.SessionKey;
        public ConcurrentBag<Character> Characters { get; }
    }
}
