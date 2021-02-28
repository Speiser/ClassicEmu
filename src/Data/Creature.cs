namespace Classic.Data
{
    public class Creature : IHasPosition
    {
        public Creature()
        {
            ID = Cryptography.Random.GetUInt64();
        }

        // TODO Add stats etc
        public ulong ID { get; set; }
        public int Model { get; set; }
        public Map Position { get; set; }
    }
}
