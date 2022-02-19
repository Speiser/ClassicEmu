using System.Collections.Concurrent;
using System.Linq;
using Classic.World.Data;
using LiteDB;
using Microsoft.Extensions.Logging;

namespace Classic.World.Services;

// TODO: One per zone?
public class CharacterService
{
    private readonly ILiteCollection<Character> characters;
    private readonly ConcurrentDictionary<ulong, Character> cache = new();
    private readonly ILogger<CharacterService> logger;

    public CharacterService(ILiteDatabase db, ILogger<CharacterService> logger)
    {
        this.characters = db.GetCollection<Character>("characters");
        this.logger = logger;
    }

    public Character GetCharacter(ulong charId)
    {
        if (!this.cache.TryGetValue(charId, out var character))
        {
            character = this.characters.FindOne(c => c.Id == charId);
            if (character is not null)
            {
                this.cache.TryAdd(charId, character);
            }
        }
        return character;
    }

    public Character GetCharacter(string name)
    {
        var character = this.cache.Values.SingleOrDefault(c => c.Name == name);
        if (character is null)
        {
            character = this.characters.FindOne(c => c.Name == name);
            if (character is not null)
            {
                this.cache.TryAdd(character.Id, character);
            }
        }
        return character;
    }

    public bool AddCharacter(Character character)
    {
        if (this.GetCharacter(character.Id) is not null)
        {
            return false;
        }

        this.characters.Insert(character);
        this.characters.EnsureIndex(c => c.Name);
        return true;
    }

    public bool DeleteCharacter(ulong charId) => this.characters.Delete(charId);

    // I really need to find a better way to handle updating the db with the cache values..
    public void Save()
    {
        this.logger.LogInformation("Writing character cache into db");
        this.characters.Upsert(this.cache.Values);
        this.cache.Clear();
        this.logger.LogInformation("Finished writing cache into db");
    }
}
