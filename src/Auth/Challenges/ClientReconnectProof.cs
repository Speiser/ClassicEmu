using System.Threading.Tasks;
using Classic.Auth.Challenges.Abstract;

namespace Classic.Auth.Challenges
{
    public class ClientReconnectProof : ClientMessageBase
    {
        // uint8    cmd
        // char[16] proof_data
        // char[20] client_proof
        // char[20] unk_hash
        // uint8    unk

        public ClientReconnectProof(byte[] packet, LoginClient client) : base(packet, client) { }
        
        public async override Task<bool> Execute()
        {
            await this.client.Send(new ServerReconnectProof().Get());
            return true;
        }
    }
}
