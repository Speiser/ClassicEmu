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
        public static async Task OnClientAuthenticationSession(WorldClient client, byte[] data)
        {
            var request = new CMSG_AUTH_SESSION(data);

            if (!DataStore.Users.TryGetValue(request.AccountName, out var user))
            {
                // return [SMSG_AUTH_RESPONSE, 21]
                throw new ArgumentException($"No user with name {request.AccountName} found in db.");
            }

            ////: if server is full and NOT GM return [SMSG_AUTH_RESPONSE, 21]
            ////: if player is already connected return [SMSG_AUTH_RESPONSE, 13]

            using (var sha = new SHA1CryptoServiceProvider())
            {
                var calculatedDigest = sha.ComputeHash(
                    Encoding.ASCII.GetBytes(request.AccountName)
                        .Concat(new byte[] { 0, 0, 0, 0 })
                        .Concat(BitConverter.GetBytes(request.Seed))
                        .Concat(SMSG_AUTH_CHALLENGE.AuthSeed)
                        .Concat(user.SessionKey)
                        .ToArray());

                if (!calculatedDigest.SequenceEqual(request.Digest))
                {
                    //return [SMSG_AUTH_RESPONSE, 21]
                    throw new InvalidOperationException("Wrong digest SMSG_AUTH_RESPONSE");
                }
            }

            client.Crypt.SetKey(user.SessionKey);
            await client.SendPacket(new SMSG_AUTH_RESPONSE());
            client.User = user;
        }
    }
}
