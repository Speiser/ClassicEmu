﻿using System;
using System.Text;
using Classic.Auth.Data.Enums;
using Classic.Shared;

namespace Classic.Auth.Challenges.Abstract
{
    public abstract class ClientChallengeBase : ClientMessageBase
    {
        protected readonly Opcode cmd;
        protected readonly AuthenticationStatus error;
        protected readonly ushort size;
        protected readonly string gameName;
        protected readonly byte majorVersion;
        protected readonly byte minorVersion;
        protected readonly byte bugfixVersion;
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
            this.error = (AuthenticationStatus)reader.ReadByte();
            this.size = reader.ReadUInt16();
            this.gameName = Encoding.ASCII.GetString(reader.ReadBytes(4));
            this.majorVersion = reader.ReadByte();
            this.minorVersion = reader.ReadByte();
            this.bugfixVersion = reader.ReadByte();
            this.build = reader.ReadUInt16();
            this.platform = Encoding.ASCII.GetString(reader.ReadBytes(4));
            this.os = Encoding.ASCII.GetString(reader.ReadBytes(4));
            this.country = Encoding.ASCII.GetString(reader.ReadBytes(4));
            this.timezone = reader.ReadUInt32();
            this.ip = reader.ReadUInt32();

            var identifierLength = Convert.ToInt32(this.packet[33]);
            this.identifier = Encoding.ASCII.GetString(((Span<byte>)this.packet).Slice(34, identifierLength));

            this.client.Build = this.build;
        }
    }
}