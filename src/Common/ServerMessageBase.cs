using System;

namespace Classic.Common
{
    public abstract class ServerMessageBase<TOpcode> : IDisposable
    {
        protected ServerMessageBase(TOpcode opcode)
        {
            this.Opcode = opcode;
            this.Writer = new PacketWriter();
        }

        public TOpcode Opcode { get; }
        protected PacketWriter Writer { get; }

        public abstract byte[] Get();

        public void Dispose() => this.Writer?.Dispose();
    }
}
