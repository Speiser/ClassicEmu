using Classic.Auth.Cryptography;
using Classic.Auth.Data.Enums;
using Classic.Shared;
using Classic.Shared.Data;

namespace Classic.Auth.Packets
{
    public class ServerLoginProof
    {
        public static byte[] Success(SecureRemotePasswordProtocol srp, int build)
        {
            using var packet = new PacketWriter();
            packet
                .WriteUInt8( /* cmd   */ (byte)Opcode.LoginProof)
                .WriteUInt8( /* error */ (byte)AuthenticationStatus.Success)
                .WriteBytes( /* M[20] */ srp.M)
                .WriteUInt32(/* unk   */ 0);

            if (build > ClientBuild.Vanilla)
            {
                packet
                    .WriteUInt32(/* unk2 */ 0)
                    .WriteUInt16(/* unk3 */ 0);
            }

            return packet.Build();
        }

        public static byte[] Failed() => new byte[] { 0, 0, 4 };
    }
}