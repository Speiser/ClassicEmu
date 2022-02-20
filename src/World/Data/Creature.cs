namespace Classic.World.Data;

public class Creature : IHasPosition
{
    public Creature()
    {
        ID = Shared.Cryptography.Random.GetUInt64();
    }

    // TODO Add stats etc
    public ulong ID { get; set; }
    public int Model { get; set; }
    public Map Position { get; set; }
    public uint Life { get; set; } = 3000;
    public uint MaxLife { get; set; } = 3000;
}
