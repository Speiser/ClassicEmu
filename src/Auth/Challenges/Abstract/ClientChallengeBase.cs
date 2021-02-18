using System;
using System.Text;
using Classic.Common;

namespace Classic.Auth.Challenges.Abstract
{
    public abstract class ClientChallengeBase : ClientMessageBase
    {
        protected readonly Opcode cmd;
        protected readonly byte error;
        protected readonly ushort size;
        protected readonly string gameName;
        protected readonly byte version1;
        protected readonly byte version2;
        protected readonly byte version3;
        protected readonly ushort build;
        protected readonly string platform;
        protected readonly string os;
        protected readonly string country;
        protected readonly uint timezone;
        protected readonly uint ip;
        protected readonly string identifier;

        public ClientChallengeBase(byte[] packet, LoginClient client) : base(packet, client)
        {
            using var reader = new PacketReader(this.packet);

            this.cmd = (Opcode)reader.ReadByte();
            this.error = reader.ReadByte();
            this.size = reader.ReadUInt16();
            this.gameName = Encoding.ASCII.GetString(reader.ReadBytes(4));
            this.version1 = reader.ReadByte();
            this.version2 = reader.ReadByte();
            this.version3 = reader.ReadByte();
            this.build = reader.ReadUInt16();
            this.platform = Encoding.ASCII.GetString(reader.ReadBytes(4));
            this.os = Encoding.ASCII.GetString(reader.ReadBytes(4));
            this.country = Encoding.ASCII.GetString(reader.ReadBytes(4));
            this.timezone = reader.ReadUInt32();
            this.ip = reader.ReadUInt32();

            this.client.GameVersion = GetGameVersion(version1, version2, version3);

            var identifierLength = Convert.ToInt32(this.packet[33]);
            this.identifier = Encoding.ASCII.GetString(((Span<byte>)this.packet).Slice(34, identifierLength));
        }

        private static GameVersion GetGameVersion(byte v1, byte v2, byte v3)
        {
            if (v1 == 1 && v2 == 12 && v3 == 1)
                return GameVersion.Classic;

            if (v1 == 3 && v2 == 3 && v3 == 5)
                return GameVersion.WotLK;

            throw new ArgumentOutOfRangeException();
        }
    }
}