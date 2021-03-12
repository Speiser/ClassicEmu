using System;

namespace Classic.Auth.Packets
{
    public class ClientLoginProof
    {
        public ClientLoginProof(byte[] packet)
        {
            var (clientPublicValue, clientProof) = GetClientValues(packet);
            this.PublicValue = clientPublicValue;
            this.Proof = clientProof;
        }

        public byte[] PublicValue { get; }
        public byte[] Proof { get; }

        private static (byte[], byte[]) GetClientValues(byte[] packet)
        {
            Span<byte> bytes = packet;
            var clientPublicValue = bytes.Slice(1, 32).ToArray();
            var clientProof = bytes.Slice(33, 20).ToArray();
            return (clientPublicValue, clientProof);
        }
    }
}