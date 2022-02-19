using System;
using System.Collections.Generic;
using Classic.World.Data.Enums.Character;
using Classic.World.Data.Enums.Map;

namespace Classic.World.Data;

public class Map
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public float Orientation { get; set; }
    public MapID ID { get; set; }
    public ZoneID Zone { get; set; }

    public Map Copy() => new Map { X = this.X, Y = this.Y, Z = this.Z, Orientation = this.Orientation, ID = this.ID, Zone = this.Zone };

    private static readonly Dictionary<Race, Func<Map>> StartingAreas = new Dictionary<Race, Func<Map>>
    {
        { Race.Human, () => new Map { X = -8949.95F, Y = -132.493F, Z = 83.5312F, Orientation = 1F, ID = MapID.EasternKingdoms, Zone = ZoneID.ElwynnForest }},
        { Race.Orc, () => new Map { X = -618.518F, Y = -4251.67F, Z = 38.718F, Orientation = 1F, ID = MapID.Kalimdor, Zone = ZoneID.Durotar }},
        { Race.Dwarf, () => new Map { X = -6240.32F, Y = 331.033F, Z = 382.758F, Orientation = 6.17716F, ID = MapID.EasternKingdoms, Zone = ZoneID.DunMorogh }},
        { Race.NightElf, () => new Map { X = 10311.3F, Y = 832.463F, Z = 1326.41F, Orientation = 5.69632F, ID = MapID.Kalimdor, Zone = ZoneID.Teldrassil }},
        { Race.Undead, () => new Map { X = 1676.35F, Y = 1677.45F, Z = 121.67F, Orientation = 3.14F, ID = MapID.EasternKingdoms, Zone = ZoneID.TirisfalGlades }},
        { Race.Tauren, () => new Map { X = -2917.58F, Y = -257.98F, Z = 52.9968F, Orientation = 1F, ID = MapID.Kalimdor, Zone = ZoneID.Mulgore }},
        { Race.Gnome, () => new Map { X = -6240.32F, Y = 331.033F, Z = 382.758F, Orientation = 1F, ID = MapID.EasternKingdoms, Zone = ZoneID.DunMorogh }},
        { Race.Troll, () => new Map { X = -618.518F, Y = -4251.67F, Z = 38.718F, Orientation = 1F, ID = MapID.Kalimdor, Zone = ZoneID.Durotar }},

        // TBC
        { Race.BloodElf, () => new Map { X = 10349.6F, Y = -6357.29F, Z = 33.4026F, Orientation = 5.31605F, ID = MapID.Outland, Zone = ZoneID.SunstriderIsle }},
        { Race.Draenei, () => new Map { X = -3961.64F, Y = -13931.2F, Z = 100.615F, Orientation = 2.08364F, ID = MapID.Outland, Zone = ZoneID.AmmenVale }},
    };

    public static Map GetStartingPosition(Race race) => StartingAreas[race]();
}
