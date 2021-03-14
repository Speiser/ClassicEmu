using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Classic.Shared.Data;
using Classic.World.Cryptography;
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

            using var sha = new SHA1CryptoServiceProvider();
            var calculatedDigest = sha.ComputeHash(
                Encoding.ASCII.GetBytes(request.Identifier)
                    .Concat(new byte[] { 0, 0, 0, 0 })
                    .Concat(BitConverter.GetBytes(request.Seed))
                    .Concat(SMSG_AUTH_CHALLENGE.AuthSeed)
                    .Concat(session.SessionKey)
                    .ToArray());

            if (!calculatedDigest.SequenceEqual(request.Digest))
            {
                //return [SMSG_AUTH_RESPONSE, 21]
                throw new InvalidOperationException("Wrong digest SMSG_AUTH_RESPONSE");
            }

            c.Client.HeaderCrypt = GenerateHeaderCrypt(session.SessionKey, build);
            c.Client.Identifier = request.Identifier;
            await c.Client.SendPacket(new SMSG_AUTH_RESPONSE(build));
        }

        private static IHeaderCrypt GenerateHeaderCrypt(byte[] sessionKey, int build)
        {
            return build switch
            {
                ClientBuild.Vanilla => new AuthCrypt(sessionKey),
                ClientBuild.TBC => new AuthCrypt(GenerateTBCCryptKey(sessionKey)),
                ClientBuild.WotLK => new ARC4(sessionKey),
                _ => throw new NotImplementedException($"GenerateHeaderCrypt(build: {build})"),
            };
        }

        private static byte[] GenerateTBCCryptKey(byte[] sessionKey)
        {
            var temp = new byte[] { 0x38, 0xA7, 0x83, 0x15, 0xF8, 0x92, 0x25, 0x30, 0x71, 0x98, 0x67, 0xB1, 0x8C, 0x4, 0xE2, 0xAA };
            using var hash = new HMACSHA1(temp);
            return hash.ComputeHash(sessionKey);
        }
    }
}
