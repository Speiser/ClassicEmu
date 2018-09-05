using System;

namespace Classic.Auth.Challenges
{
    public class ClientLogonProof : ClientLogonBase
    {
        public ClientLogonProof(byte[] packet, LoginClient client) : base(packet, client) { }
        
        /// <summary>
        /// Exectues the <see cref="ClientLogonProof"/>.
        /// </summary>
        /// <returns>
        /// TRUE: If provided client proof was correct.
        /// FALSE: Client proof was incorrect.
        /// </returns>
        public override bool Execute()
        {
            Span<byte> bytes = this.packet;
            var clientPublicValue = bytes.Slice(1, 32).ToArray(); // TODO: CHECK AGAIN!
            var clientProof = bytes.Slice(33, 20).ToArray(); // TODO: CHECK AGAIN!;
            var data = new ServerLogonProof(this.client.SRP).Get(clientPublicValue, clientProof);
            this.client.Send(data);
            return data.Length != 3;
        }
    }
}