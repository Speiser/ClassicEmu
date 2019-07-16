namespace Classic.Data
{
    public class Map
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Orientation { get; set; }
        public int ID { get; set; }
        public int Zone { get; set; }

        public static Map Default => new Map
        {
            X = -8919.284180F,
            Y = -117.894028F,
            Z = 82.339821F,
            Orientation = 1F,
            ID = 7,
            Zone = 12
        };
    }
}
