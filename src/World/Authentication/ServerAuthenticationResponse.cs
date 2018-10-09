using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Classic.Common;
using Classic.Cryptography;
using static Classic.World.Opcode;

namespace Classic.World.Authentication
{
    public class ServerAuthenticationResponse
    {
        private readonly AuthCrypt crypt;
        public ServerAuthenticationResponse(AuthCrypt crypt)
        {
            this.crypt = crypt;
        }

        public byte[] Get(ClientAuthenticationSession recv)
        {
            if (!DataStore.Users.TryGetValue(recv.account_name, out var user))
            {
                // return [SMSG_AUTH_RESPONSE, 21]
                throw new ArgumentException($"No user with name {recv.account_name} found in db.");
            }

            ////: if server is full and NOT GM return [SMSG_AUTH_RESPONSE, 21]
            ////: if player is already connected return [SMSG_AUTH_RESPONSE, 13]

            var sha = new SHA1CryptoServiceProvider();

            var calculatedDigest = sha.ComputeHash(
                Encoding.ASCII.GetBytes(recv.account_name)
                    .Concat(new byte[] { 0, 0, 0, 0 })
                    .Concat(BitConverter.GetBytes(recv.seed))
                    .Concat(ServerAuthenticationChallenge.AuthSeed)
                    .Concat(user.SessionKey)
                    .ToArray());

            if (!calculatedDigest.SequenceEqual(recv.digest))
            {
                throw new InvalidOperationException("Wrong digest SMSG_AUTH_RESPONSE");
                //return [SMSG_AUTH_RESPONSE, 21]
            }

            this.crypt.SetKey(user.SessionKey);

            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    // Packet data
                    bw.Write((byte)12); // Result (12 = AUTH_OK)
                    bw.Write((uint)0);  // Next 3 are billing info
                    bw.Write((byte)0);
                    bw.Write((uint)0);

                    return ms.ToArray();
                }
            }
        }
    }
}
