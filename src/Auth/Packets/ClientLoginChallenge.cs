using System;
using Classic.Shared;
using Classic.Auth.Packets.Enums;
using System.Text;

namespace Classic.Auth.Packets
{
    public class ClientLoginChallenge
    {
        public ClientLoginChallenge(byte[] packet)
        {
            using var reader = new PacketReader(packet);

            this.Opcode = (Opcode)reader.ReadByte();
            this.Status = (AuthenticationStatus)reader.ReadByte();
            this.Size = reader.ReadUInt16();
            this.GameName = Encoding.ASCII.GetString(reader.ReadBytes(4));
            this.MajorVersion = reader.ReadByte();
            this.MinorVersion = reader.ReadByte();
            this.BugfixVersion = reader.ReadByte();
            this.Build = reader.ReadUInt16();
            this.Platform = Encoding.ASCII.GetString(reader.ReadBytes(4));
            this.OS = Encoding.ASCII.GetString(reader.ReadBytes(4));
            this.Country = Encoding.ASCII.GetString(reader.ReadBytes(4));
            this.Timezone = reader.ReadUInt32();
            this.IP = reader.ReadUInt32();

            var identifierLength = Convert.ToInt32(packet[33]);
            this.Identifier = Encoding.ASCII.GetString(((Span<byte>)packet).Slice(34, identifierLength));
        }

        public Opcode Opcode { get; }
        public AuthenticationStatus Status { get; }
        public ushort Size { get; }
        public string GameName { get; }
        public byte MajorVersion { get; }
        public byte MinorVersion { get; }
        public byte BugfixVersion { get; }
        public ushort Build { get; }
        public string Platform { get; }
        public string OS { get; }
        public string Country { get; }
        public uint Timezone { get; }
        public uint IP { get; }
        public string Identifier { get; }
    }
}