using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Classic.Common;
using Classic.World.Messages.Client;
using Classic.World.Messages.Server;

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
                throw new ArgumentException($"Expected build {args.Client.Build} but is {build}.");
            }

            if (!DataStore.Users.TryGetValue(request.AccountName, out var user))
            {
                // return [SMSG_AUTH_RESPONSE, 21]
                throw new ArgumentException($"No user with name {request.AccountName} found in db.");
            }

            ////: if server is full and NOT GM return [SMSG_AUTH_RESPONSE, 21]
            ////: if player is already connected return [SMSG_AUTH_RESPONSE, 13]

            if (build == ClientBuild.Vanilla || build == ClientBuild.TBC)
            {
                using (var sha = new SHA1CryptoServiceProvider())
                {
                    var calculatedDigest = sha.ComputeHash(
                        Encoding.ASCII.GetBytes(request.AccountName)
                            .Concat(new byte[] { 0, 0, 0, 0 })
                            .Concat(BitConverter.GetBytes(request.Seed))
                            .Concat(SMSG_AUTH_CHALLENGE_VANILLA_TBC.AuthSeed)
                            .Concat(user.SessionKey)
                            .ToArray());

                    if (!calculatedDigest.SequenceEqual(request.Digest))
                    {
                        //return [SMSG_AUTH_RESPONSE, 21]
                        throw new InvalidOperationException("Wrong digest SMSG_AUTH_RESPONSE");
                    }
                }
            }

            if (build == ClientBuild.Vanilla)
            {
                args.Client.Crypt.SetKey(user.SessionKey);
            }

            await args.Client.SendPacket(new SMSG_AUTH_RESPONSE());
            args.Client.User = user;
        }
    }
}
