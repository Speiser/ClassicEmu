namespace Classic.World.Data;

public class Spell
{
    public int Id { get; set; }

    public static Spell Fireball() => new Spell { Id = 133 };
}
