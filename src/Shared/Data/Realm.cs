using Classic.Shared.Data.Enums;
using LiteDB;

namespace Classic.Shared.Data;

public class Realm
{
    public ObjectId Id { get; set; }
    public RealmType Type { get; set; }
    public byte Lock { get; set; }
    public RealmFlag Flags { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public uint Population { get; set; }
    public byte TimeZone { get; set; }
}
