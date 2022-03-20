using Classic.Shared;
using Classic.World.Entities.Enums;

namespace Classic.World.Packets.Client;

public class CMSG_SETSHEATHED
{
    public CMSG_SETSHEATHED(byte[] data)
    {
        using var reader = new PacketReader(data);
        this.Sheated = (SheathState)reader.ReadUInt32();
    }

    public SheathState Sheated { get; }
}
