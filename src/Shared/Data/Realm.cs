using Classic.Shared.Data.Enums;

namespace Classic.Shared.Data;

public class Realm
{
    public RealmType Type { get; set; }
    public byte Lock { get; set; }
    public RealmFlag Flags { get; set; }
    public string Name { get; set; }
    public string AddressVanilla { get; set; }
    public string AddressTBC { get; set; }
    public string AddressWotLK { get; set; }
    public uint Population { get; set; }
    public byte TimeZone { get; set; }
}

public class PRealm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public short PortVanilla { get; set; }
    public short PortTbc { get; set; }
    public short PortWotlk { get; set; }
    public byte Type { get; set; }
    public byte Flags { get; set; }
    public int Population { get; set; }
    public byte Timezone { get; set; }
}
