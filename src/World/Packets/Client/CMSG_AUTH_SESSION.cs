using Classic.Shared;
using Classic.Shared.Data;

namespace Classic.World.Packets.Client;

public abstract class CMSG_AUTH_SESSION
{
    public uint Build { get; protected set; }
    public uint Session { get; protected set; }
    public string Identifier { get; protected set; }
    public uint Seed { get; protected set; }
    public byte[] Digest { get; protected set; }

    public static CMSG_AUTH_SESSION Read(byte[] data)
    {
        using var reader = new PacketReader(data);
        var build = (int)reader.ReadUInt32();
        if (build == ClientBuild.Vanilla || build == ClientBuild.TBC)
        {
            return new CMSG_AUTH_SESSION_VANILLA_TBC(data);
        }
        return new CMSG_AUTH_SESSION_WOTLK(data);
    }
}

public class CMSG_AUTH_SESSION_VANILLA_TBC : CMSG_AUTH_SESSION
{
    public CMSG_AUTH_SESSION_VANILLA_TBC(byte[] data)
    {
        using var reader = new PacketReader(data);
        base.Build = reader.ReadUInt32();
        this.Session = reader.ReadUInt32();
        base.Identifier = reader.ReadString();
        base.Seed = reader.ReadUInt32();
        base.Digest = reader.ReadBytes(20);
        this.AddonSize = reader.ReadUInt32();
    }

    public uint AddonSize { get; }
}

public class CMSG_AUTH_SESSION_WOTLK : CMSG_AUTH_SESSION
{
    public CMSG_AUTH_SESSION_WOTLK(byte[] data)
    {
        using var reader = new PacketReader(data);
        base.Build = reader.ReadUInt32();
        this.Session = reader.ReadUInt32();
        base.Identifier = reader.ReadString();
        var unk1 = reader.ReadUInt32();
        base.Seed = reader.ReadUInt32();
        var unk2 = reader.ReadUInt32();
        var unk3 = reader.ReadUInt32();
        var unk4 = reader.ReadUInt32();
        var unk5 = reader.ReadUInt64();
        base.Digest = reader.ReadBytes(20);
    }
}
