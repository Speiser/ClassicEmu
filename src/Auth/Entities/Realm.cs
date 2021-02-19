namespace Classic.Auth.Entities
{
    public class Realm
    {
        public int Type { get; set; }
        public byte Lock { get; set; }
        public byte Flags { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public uint Population { get; set; }
        public byte TimeZone { get; set; }
    }
}