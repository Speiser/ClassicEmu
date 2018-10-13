using Classic.Common;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Classic.World.Handler;
using static Classic.World.Opcode;

namespace Classic.World
{
    public class WorldServer : ServerBase
    {
        public WorldServer() : base(new IPEndPoint(IPAddress.Loopback, 13250))
        {
            // Register packet handlers
            // TODO: Reflection and attributes?
            WorldPacketHandler.Register(CMSG_AUTH_SESSION, AuthenticationHandler.OnClientAuthenticationSession);
            WorldPacketHandler.Register(CMSG_CHAR_ENUM, CharacterHandler.OnCharacterEnum);
            WorldPacketHandler.Register(CMSG_CHAR_CREATE, CharacterHandler.OnCharacterCreate);
            WorldPacketHandler.Register(CMSG_PLAYER_LOGIN, PlayerHandler.OnPlayerLogin);
        }

        protected override void ProcessClient(TcpClient client)
        {
            new Thread(() => new WorldClient(client).HandleConnection()).Start();
        }
    }
}
