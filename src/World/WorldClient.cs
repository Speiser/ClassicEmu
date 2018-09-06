using Classic.Common;
using Classic.World.Challenges;
using System.Net.Sockets;

namespace Classic.World
{
    public class WorldClient : ClientBase
    {
        public WorldClient(TcpClient client) : base(client)
        {
            this.Log("-- connected");
            this.Send(new ServerAuthenticationChallenge().Get());
        }

        protected override void HandlePacket(byte[] packet)
        {
            this.LogPacket(packet);
        }
    }
}
