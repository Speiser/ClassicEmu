using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Classic.Common;
using Classic.World.Authentication;
using static Classic.World.Opcode;

namespace Classic.World.Handler
{
    public class AuthenticationHandler
    {
        public static void OnClientAuthenticationSession(WorldClient client, byte[] data)
        {
            var reader = new PacketReader(data);
            ushort len = reader.ReadUInt16Reverse();

            if (len != data.Length - 2)
                throw new ArgumentOutOfRangeException(nameof(data), "Packet length mismatch");

            ushort cmd = reader.ReadUInt16();

            if (cmd != (ushort)CMSG_AUTH_SESSION)
                throw new ArgumentOutOfRangeException(nameof(data), "Packet length mismatch");

            ushort unk1 = reader.ReadUInt16();
            uint build = reader.ReadUInt32();
            uint session = reader.ReadUInt32();
            string account_name = reader.ReadString();
            uint seed = reader.ReadUInt32();
            byte[] digest = reader.ReadBytes(20);
            uint addon_size = reader.ReadUInt32();

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
                    .Concat(ServerAuthenticationChallenge.AuthSeed)
                    .Concat(user.SessionKey)
                    .ToArray());

            if (!calculatedDigest.SequenceEqual(digest))
            {
                throw new InvalidOperationException("Wrong digest SMSG_AUTH_RESPONSE");
                //return [SMSG_AUTH_RESPONSE, 21]
            }

            client.Crypt.SetKey(user.SessionKey);

            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    // Packet data
                    bw.Write((byte)12); // Result (12 = AUTH_OK)
                    bw.Write((uint)0);  // Next 3 are billing info
                    bw.Write((byte)0);
                    bw.Write((uint)0);

                    client.SendPacket(ms.ToArray(), SMSG_AUTH_RESPONSE);
                }
            }

            client.User = user;
        }
    }
}
