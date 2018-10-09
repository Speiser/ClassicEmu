using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Classic.Common;
using static Classic.World.Opcode;

namespace Classic.World.Authentication
{
    public class ClientAuthenticationSession
    {
        /* Trying to understand the received package data...
         * uint16 len;
         * uint16 cmd;
         * uint16 unk1;
         * uint32 build;
         * uint32 session;
         * uint8  account_name[];
         * uint32 seed;
         * uint8  digest[];
         * uint32 addon_size;
         */

        public readonly ushort len;
        public readonly ushort cmd;
        public readonly ushort unk1;
        public readonly uint build;
        public readonly uint session;
        public readonly string account_name;
        public readonly uint seed;
        public readonly byte[] digest; // 20
        public readonly uint addon_size;

        public ClientAuthenticationSession(byte[] packet)
        {
            using (var reader = new PacketReader(packet))
            {
                len = reader.ReadUInt16Reverse();

                if (len != packet.Length - 2)
                    throw new ArgumentOutOfRangeException(nameof(packet), "Packet length mismatch");

                cmd = reader.ReadUInt16();

                if (cmd != (ushort)CMSG_AUTH_SESSION)
                    throw new ArgumentOutOfRangeException(nameof(packet), "Packet length mismatch");

                unk1 = reader.ReadUInt16();
                build = reader.ReadUInt32();
                session = reader.ReadUInt32();
                account_name = reader.ReadString();
                seed = reader.ReadUInt32();
                digest = reader.ReadBytes(20);
                addon_size = reader.ReadUInt32();
            }
        }

        public override string ToString()
        {

            return $"uint16 len = {len}\n" +
                   $"uint16 cmd = {cmd}\n" +
                   $"uint16 unk1 = {unk1}\n" +
                   $"uint32 build = {build}\n" +
                   $"uint32 session = {session}\n" +
                   $"uint8  account_name[] = {account_name}\n" +
                   $"uint32 seed = {seed}\n" +
                   $"uint8  digest[] = {string.Join(" ", digest)}\n" +
                   $"uint32 addon_size = {addon_size}";
        }
    }
}
