using Classic.Auth.Data.Enums;
using Classic.Shared;

namespace Classic.Auth.Challenges
{
    public class ServerReconnectProof
    {
        public byte[] Get() => new PacketWriter()
            .WriteUInt8((byte)Opcode.ReconnectProof)
            .WriteUInt8((byte)AuthenticationStatus.Success)
            .WriteUInt16(0) // 2 zeros?
            .Build(); 
    }
}
