namespace Classic.Data
{
    public class Skill
    {
        public int Id { get; set; }
        public int Current { get; set; }
        public int Max { get; set; }

        public static Skill Staves() => new Skill { Id = 136, Current = 1, Max = 5 };
    }
}
