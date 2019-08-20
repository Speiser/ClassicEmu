using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Classic.Common;
using Classic.World.Messages;

namespace Classic.World.Handler
{
    public class AuthenticationHandler
    {
        [OpcodeHandler(Opcode.CMSG_AUTH_SESSION)]
        public static async Task OnClientAuthenticationSession(WorldClient client, byte[] data)
        {
            var reader = new PacketReader(data);

            var build = reader.ReadUInt32();
            var session = reader.ReadUInt32();
            var account_name = reader.ReadString();
            var seed = reader.ReadUInt32();
            var digest = reader.ReadBytes(20);
            var addon_size = reader.ReadUInt32();

            reader.Dispose();

            if (!DataStore.Users.TryGetValue(account_name, out var user))
            {
                // return [SMSG_AUTH_RESPONSE, 21]
                throw new ArgumentException($"No user with name {account_name} found in db.");
            }

            ////: if server is full and NOT GM return [SMSG_AUTH_RESPONSE, 21]
            ////: if player is already connected return [SMSG_AUTH_RESPONSE, 13]

            var sha = new SHA1CryptoServiceProvider();

            var calculatedDigest = sha.ComputeHash(
                Encoding.ASCII.GetBytes(account_name)
                    .Concat(new byte[] { 0, 0, 0, 0 })
                    .Concat(BitConverter.GetBytes(seed))
                    .Concat(SMSG_AUTH_CHALLENGE.AuthSeed)
                    .Concat(user.SessionKey)
                    .ToArray());

            if (!calculatedDigest.SequenceEqual(digest))
            {
                //return [SMSG_AUTH_RESPONSE, 21]
                throw new InvalidOperationException("Wrong digest SMSG_AUTH_RESPONSE");
            }

            client.Crypt.SetKey(user.SessionKey);
            await client.SendPacket(new SMSG_AUTH_RESPONSE());
            client.User = user;
        }
    }
}
