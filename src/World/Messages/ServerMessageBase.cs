using System;
using Classic.Shared;

namespace Classic.World.Messages
{
    public abstract class ServerMessageBase<TOpcode> : IDisposable
    {
        protected ServerMessageBase(TOpcode opcode)
        {
            this.Opcode = opcode;
            this.Writer = new PacketWriter();
        }

        public TOpcode Opcode { get; set; }
        public PacketWriter Writer { get; set; }

        public abstract byte[] Get();

        public void Dispose() => this.Writer?.Dispose();
    }
}
