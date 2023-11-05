using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Classic.World.Data;
using Dapper;

namespace Classic.World.Services;

// TODO: One per zone?
public class CharacterService
{
    private readonly ConcurrentDictionary<ulong, Character> cache = new();
    private readonly WorldDatabase worldDatabase;

    public CharacterService(WorldDatabase worldDatabase)
    {
        this.worldDatabase = worldDatabase;
    }

    public async Task<IEnumerable<Character>> GetAccountCharacters(int accountId)
    {
        using var connection = this.worldDatabase.GetConnection();
        var characters = await connection.QueryAsync<PCharacter>("SELECT * FROM characters WHERE account_id = @AccountId", new { AccountId = accountId });
        return characters.Select(c => new Character(c));
    }

    public async Task<Character> GetCharacter(ulong characterId)
    {
        if (!this.cache.TryGetValue(characterId, out var character))
        {
            using var connection = this.worldDatabase.GetConnection();
            var pCharacter = await connection.QueryFirstOrDefaultAsync<PCharacter>("SELECT * FROM characters WHERE id = @CharacterId", new
            {
                CharacterId = (int)characterId,
            });

            character = pCharacter is null ? null : new Character(pCharacter);
            if (character is not null)
            {
                this.cache.TryAdd(character.Id, character);
            }
        }
        return character;
    }

    public async Task<Character> GetCharacter(string name)
    {
        var character = this.cache.Values.SingleOrDefault(c => c.Name == name);
        if (character is null)
        {
            using var connection = this.worldDatabase.GetConnection();
            var pCharacter = await connection.QueryFirstOrDefaultAsync<PCharacter>("SELECT * FROM characters WHERE name = @Name", new
            {
                Name = name,
            });

            character = pCharacter is null ? null : new Character(pCharacter);
            if (character is not null)
            {
                this.cache.TryAdd(character.Id, character);
            }
        }

        return character;
    }

    public async Task<bool> AddCharacter(PCharacter character)
    {
        using var connection = this.worldDatabase.GetConnection();
        await connection.ExecuteAsync(@"
INSERT INTO characters
(
    account_id, name, race, class, gender, skin, face, hair_style, hair_color,
    facial_hair, outfit_id, position_x, position_y, position_z, position_o,
    map_id, zone_id, flag, stand_state
)
VALUES
(
    @AccountId, @Name, @Race, @Class, @Gender, @Skin, @Face, @HairStyle, @HairColor,
    @FacialHair, @OutfitId, @PositionX, @PositionY, @PositionZ, @PositionO,
    @MapId, @ZoneId, @Flag, @StandState
);",
        new
        {
            character.AccountId,
            character.Name,
            character.Race,
            character.Class,
            character.Gender,
            character.Skin,
            character.Face,
            character.HairStyle,
            character.HairColor,
            character.FacialHair,
            character.OutfitId,
            character.PositionX,
            character.PositionY,
            character.PositionZ,
            character.PositionO,
            character.MapId,
            character.ZoneId,
            character.Flag,
            character.StandState, // TODO Default
        });
        return true; // TODO Check if exists
    }

    public async Task<bool> DeleteCharacter(ulong charId)
    {
        using var connection = this.worldDatabase.GetConnection();
        var rowsAffected = await connection.ExecuteAsync("DELETE FROM characters WHERE id = @Id;", new { Id = (int)charId });
        return rowsAffected == 1;
    }

    // I really need to find a better way to handle updating the db with the cache values..
    public void Save()
    {
        // TODO: How to update all??
        //this.logger.LogInformation("Writing character cache into db");
        //this.characters.Upsert(this.cache.Values);
        //this.cache.Clear();
        //this.logger.LogInformation("Finished writing cache into db");
    }
}
