namespace Classic.World.Data;

public class ActionBarItem
{
    public int SpellId { get; set; }
    public int Type { get; set; }

    // TODO: Move to db ;)
    public static ActionBarItem Fireball() => new() { SpellId = 133, Type = 0 };
    public static ActionBarItem Pyroblast() => new() { SpellId = 11366, Type = 0 };
    public static ActionBarItem ArcaneMissiles() => new() { SpellId = 5143, Type = 0 };
}
