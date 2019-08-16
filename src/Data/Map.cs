using System.Collections.Generic;
using Classic.Data.CharacterEnums;

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

        public static Dictionary<Race, Map> StartingAreas = new Dictionary<Race, Map> {
            // TODO: Load from file??
            { Race.Human, new Map { X = -8949.95F, Y = -132.493F, Z = 83.5312F, Orientation = 1F, ID = 0, Zone = 12 }}
        };

        public static Map Default => StartingAreas[Race.Human];
    }
}
