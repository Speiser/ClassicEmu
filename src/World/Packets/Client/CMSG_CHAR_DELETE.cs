using Classic.Shared;

namespace Classic.World.Packets.Client;

public class CMSG_CHAR_DELETE
{
    public CMSG_CHAR_DELETE(byte[] data)
    {
        using (var reader = new PacketReader(data))
        {
            CharacterId = reader.ReadUInt64();
        }
    }

    public ulong CharacterId { get; }
}
