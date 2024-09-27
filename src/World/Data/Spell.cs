namespace Classic.World.Data;

public class Spell
{
    public int Id { get; set; }

    public static Spell Fireball() => new() { Id = 133 };
    public static Spell Pyroblast() => new() { Id = 11366 };
    public static Spell ArcaneMissiles() => new() { Id = 5143 };
}
