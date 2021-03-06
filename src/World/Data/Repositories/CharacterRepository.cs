using LiteDB;

namespace Classic.World.Data.Repositories
{
    public class CharacterRepository
    {
        private readonly ILiteCollection<Character> characters;

        public CharacterRepository(ILiteDatabase db)
        {
            this.characters = db.GetCollection<Character>("characters");
        }

        public Character GetCharacter(ulong charId) => this.characters.FindOne(c => c.Id == charId);

        public bool AddCharacter(Character character)
        {
            if (this.GetCharacter(character.Id) is not null)
            {
                return false;
            }

            this.characters.Insert(character);
            return true;
        }

        public bool DeleteCharacter(ulong charId) => this.characters.Delete(charId);
    }
}
