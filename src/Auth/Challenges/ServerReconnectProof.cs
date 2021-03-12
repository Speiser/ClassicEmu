using Classic.Auth.Data.Enums;
using Classic.Shared;
using static Classic.Auth.Opcode;

namespace Classic.Auth.Challenges
{
    public class ServerReconnectProof
    {
        public byte[] Get() => new PacketWriter()
            .WriteUInt8((byte)RECONNECT_PROOF)
            .WriteUInt8((byte)AuthenticationStatus.Success)
            .WriteUInt16(0) // 2 zeros?
            .Build(); 
    }
}
