namespace Classic.World.Data;

public class ActionBarItem
{
    public int SpellId { get; set; }
    public int Type { get; set; }

    public static ActionBarItem Fireball() => new ActionBarItem { SpellId = 133, Type = 0 };
}
