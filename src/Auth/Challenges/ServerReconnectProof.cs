using Classic.Shared;
using static Classic.Auth.Opcode;

namespace Classic.Auth.Challenges
{
    public class ServerReconnectProof
    {
        public byte[] Get() => new PacketWriter()
            .WriteUInt8((byte)RECONNECT_PROOF) // cmd
            .WriteUInt8(0)  // error (0 is success)
            .WriteUInt16(0) // 2 zeros?
            .Build(); 
    }
}
