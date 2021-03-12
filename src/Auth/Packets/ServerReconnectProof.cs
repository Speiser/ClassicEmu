using Classic.Auth.Data.Enums;
using Classic.Shared;

namespace Classic.Auth.Packets
{
    public class ServerReconnectProof
    {
        public static byte[] Success() => new PacketWriter()
            .WriteUInt8((byte)Opcode.ReconnectProof)
            .WriteUInt8((byte)AuthenticationStatus.Success)
            .WriteUInt16(0) // 2 zeros?
            .Build(); 
    }
}
