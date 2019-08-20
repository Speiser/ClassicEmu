using System;
using System.Text;
using System.Threading.Tasks;
using Classic.Cryptography;

namespace Classic.Auth.Challenges
{
    public class ClientLogonChallenge : ClientLogonBase
    {
        public ClientLogonChallenge(byte[] packet, LoginClient client) : base(packet, client) { }

        public override async Task<bool> Execute()
        {
            var identifier = this.GetIdentifier();
            this.client.SRP = new SecureRemotePasswordProtocol(identifier, identifier); // TODO: Quick hack

            // Create and send a ServerLogonChallenge as response.
            await this.client.Send(new ServerLogonChallenge(this.client.SRP).Get());
            return true;
        }

        private string GetIdentifier()
        {
            // TODO: Parse all received data?
            // Only doing necessary for now..
            var identifierLength = Convert.ToInt32(this.packet[33]);
            return Encoding.ASCII.GetString(((Span<byte>) this.packet).Slice(34, identifierLength));
        }
    }
}