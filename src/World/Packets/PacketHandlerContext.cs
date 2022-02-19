using Classic.Shared.Services;

namespace Classic.World.Packets;

public class PacketHandlerContext
{
    public WorldClient Client { get; init; }
    public Opcode Opcode { get; init; }
    public byte[] Packet { get; init; }
    public IWorldManager World { get; init; }
    public AccountService AccountService { get; init; }
}
