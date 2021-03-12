using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Classic.Shared.Data;
using Classic.World.Packets;
using Classic.World.Packets.Client;
using Classic.World.Packets.Server;
using Microsoft.Extensions.Logging;

namespace Classic.World.Handler
{
    public class AuthenticationHandler
    {
        [OpcodeHandler(Opcode.CMSG_AUTH_SESSION)]
        public static async Task OnClientAuthenticationSession(PacketHandlerContext c)
        {
            var (build, request) = CMSG_AUTH_SESSION.Read(c.Packet);

            if (c.Client.Build != build)
            {
                c.Client.Log($"Expected build {c.Client.Build} but is {build}.", LogLevel.Warning);
                c.Client.Build = build;
            }

            var session = c.AccountService.GetSession(request.Identifier);

            if (session is null)
            {
                // return [SMSG_AUTH_RESPONSE, 21]
                throw new ArgumentException($"No user with name {request.Identifier} found in db.");
            }

            ////: if server is full and NOT GM return [SMSG_AUTH_RESPONSE, 21]
            ////: if player is already connected return [SMSG_AUTH_RESPONSE, 13]

            if (build == ClientBuild.Vanilla || build == ClientBuild.TBC)
            {
                using var sha = new SHA1CryptoServiceProvider();
                var calculatedDigest = sha.ComputeHash(
                    Encoding.ASCII.GetBytes(request.Identifier)
                        .Concat(new byte[] { 0, 0, 0, 0 })
                        .Concat(BitConverter.GetBytes(request.Seed))
                        .Concat(SMSG_AUTH_CHALLENGE_VANILLA_TBC.AuthSeed)
                        .Concat(session.SessionKey)
                        .ToArray());

                if (!calculatedDigest.SequenceEqual(request.Digest))
                {
                    //return [SMSG_AUTH_RESPONSE, 21]
                    throw new InvalidOperationException("Wrong digest SMSG_AUTH_RESPONSE");
                }

                c.Client.Crypt.SetKey(GenerateAuthCryptKey(session.SessionKey, build));
            }

            c.Client.Identifier = request.Identifier;
            await c.Client.SendPacket(new SMSG_AUTH_RESPONSE(build));
        }

        // TODO: How to use with wotlk?
        private static byte[] GenerateAuthCryptKey(byte[] sessionKey, int build)
        {
            switch (build)
            {
                case ClientBuild.Vanilla:
                    return sessionKey;
                case ClientBuild.TBC:
                    // https://github.com/SunstriderEmu/sunstrider-legacy/blob/efd728a145c29f5e322aca7d388d6f039fd28ab2/src/common/Cryptography/Authentication/AuthCrypt.cpp#L41
                    var temp = new byte[] { 0x38, 0xA7, 0x83, 0x15, 0xF8, 0x92, 0x25, 0x30, 0x71, 0x98, 0x67, 0xB1, 0x8C, 0x4, 0xE2, 0xAA };
                    var hash = new HMACSHA1(temp);
                    return hash.ComputeHash(sessionKey);
                default:
                    throw new NotImplementedException($"GenerateAuthCryptKey(build: {build})");
            }
        }
    }
}
