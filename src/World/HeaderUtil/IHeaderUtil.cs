using Classic.Common;

namespace Classic.World.HeaderUtil
{
    public interface IHeaderUtil
    {
        byte[] Encode(ServerMessageBase<Opcode> message);
    }
}
