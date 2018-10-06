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

            #region UNNECESSARY (CURRENTLY)
            ////: if server is full and NOT GM return [SMSG_AUTH_RESPONSE, 21]
            ////: if player is already connected return [SMSG_AUTH_RESPONSE, 13]

            //var authSeed = new byte[] { 0x33, 0x18, 0x34, 0xC8 }; // From ServerAuthChallenge
            //var sha = new SHA1CryptoServiceProvider();

            //var calculatedDigest = sha.ComputeHash(
            //    Encoding.ASCII.GetBytes(recv.account_name)
            //        .Concat(new byte[] { 0, 0, 0, 0 })
            //        .Concat(BitConverter.GetBytes(recv.seed))
            //        .Concat(authSeed)
            //        .Concat(user.SessionKey.ToByteArray())
            //        .ToArray());

            //if (calculatedDigest != recv.digest)
            //{
            //    // TODO: Calculating this correctly should NOT be necessary atm.
            //    throw new InvalidOperationException("Wrong digest SMSG_AUTH_RESPONSE");
            //    //return [SMSG_AUTH_RESPONSE, 21]
            //}
            #endregion

            this.crypt.SetKey(user.SessionKey.ToByteArray()); // should be len 40


            byte[] temp = BitConverter.GetBytes((ushort)5);
            Array.Reverse(temp); // Converting temp to big endian.

            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    // Auth failed
                    bw.Write(temp); // Size
                    bw.Write((ushort)SMSG_AUTH_RESPONSE);
                    bw.Write((ushort)0);
                    bw.Write((byte)0x0D);
                    return ms.ToArray();

                    //// Encrypt first 4 bytes of header
                    //// Packet header
                    //bw.Write(temp); // Size without header?
                    //bw.Write((ushort)SMSG_AUTH_RESPONSE); // Opcode
                    //bw.Write((byte)0); // Random uint16
                    //bw.Write((byte)0);

                    //// Packet data
                    //bw.Write((byte)12); // Result (12 = AUTH_OK)
                    //bw.Write((uint)0);  // Next 3 are billing info
                    //bw.Write((byte)0);
                    //bw.Write((uint)0);

                    //return this.crypt.EncryptSend(ms.ToArray(), 4);
                }
            }
        }
    }
}
