using Classic.World.Messages;

namespace Classic.World.HeaderUtil
{
    public interface IHeaderUtil
    {
        byte[] Encode(ServerMessageBase<Opcode> message);
    }
}
