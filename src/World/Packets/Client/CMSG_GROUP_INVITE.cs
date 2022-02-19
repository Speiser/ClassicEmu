using Classic.Shared;

namespace Classic.World.Packets.Client;

public class CMSG_GROUP_INVITE
{
    public CMSG_GROUP_INVITE(byte[] data)
    {
        using var reader = new PacketReader(data);
        this.Membername = reader.ReadString();
    }

    public string Membername { get; }
}
