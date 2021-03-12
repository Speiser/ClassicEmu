using Classic.World.Packets;

namespace Classic.World.HeaderUtil
{
    public interface IHeaderUtil
    {
        byte[] Encode(ServerPacketBase<Opcode> message);
    }
}
