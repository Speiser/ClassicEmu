using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Classic.Shared;
using Classic.Shared.Data;
using Classic.World.Messages.Client;
using Classic.World.Messages.Server;
using Microsoft.Extensions.Logging;

namespace Classic.World.Handler
{
    public class AuthenticationHandler
    {
        [OpcodeHandler(Opcode.CMSG_AUTH_SESSION)]
        public static async Task OnClientAuthenticationSession(HandlerArguments args)
        {
            var (build, request) = CMSG_AUTH_SESSION.Read(args.Data);

            if (args.Client.Build != build)
            {
                args.Client.Log($"Expected build {args.Client.Build} but is {build}.", LogLevel.Warning);
                args.Client.Build = build;
            }

            var session = AccountStore.GetSession(request.AccountName);

            if (session is null)
            {
                // return [SMSG_AUTH_RESPONSE, 21]
                throw new ArgumentException($"No user with name {request.AccountName} found in db.");
            }

            ////: if server is full and NOT GM return [SMSG_AUTH_RESPONSE, 21]
            ////: if player is already connected return [SMSG_AUTH_RESPONSE, 13]

            if (build == ClientBuild.Vanilla || build == ClientBuild.TBC)
            {
                using var sha = new SHA1CryptoServiceProvider();
                var calculatedDigest = sha.ComputeHash(
                    Encoding.ASCII.GetBytes(request.AccountName)
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

                args.Client.Crypt.SetKey(GenerateAuthCryptKey(session.SessionKey, build));
            }

            await args.Client.SendPacket(new SMSG_AUTH_RESPONSE(build));
            args.Client.Session = session;
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
