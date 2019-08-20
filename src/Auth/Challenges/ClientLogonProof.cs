using System;
using System.Threading.Tasks;

namespace Classic.Auth.Challenges
{
    public class ClientLogonProof : ClientLogonBase
    {
        public ClientLogonProof(byte[] packet, LoginClient client) : base(packet, client) { }
        
        /// <summary>
        /// Executes the <see cref="ClientLogonProof"/>.
        /// </summary>
        /// <returns>
        /// TRUE: If provided client proof was correct.
        /// FALSE: Client proof was incorrect.
        /// </returns>
        public override async Task<bool> Execute()
        {
            var (clientPublicValue, clientProof) = GetClientValues(this.packet);
            var data = new ServerLogonProof(this.client.SRP).Get(clientPublicValue, clientProof);
            await this.client.Send(data);
            return data.Length != 3;
        }

        private static (byte[], byte[]) GetClientValues(byte[] packet)
        {
            Span<byte> bytes = packet;
            var clientPublicValue = bytes.Slice(1, 32).ToArray(); // TODO: CHECK AGAIN!
            var clientProof = bytes.Slice(33, 20).ToArray(); // TODO: CHECK AGAIN!;
            return (clientPublicValue, clientProof);
        }
    }
}