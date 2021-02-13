namespace Classic.Data
{
    public class Creature
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
