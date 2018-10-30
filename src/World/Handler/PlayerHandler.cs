using System.Linq;
using Classic.Common;
using static Classic.World.Opcode;

namespace Classic.World.Handler
{
    public static class PlayerHandler
    {
        [OpcodeHandler(CMSG_PLAYER_LOGIN)]
        public static void OnPlayerLogin(WorldClient client, byte[] data)
        {
            uint charId = 0;

            using (var reader = new PacketReader(data))
            {
                reader.Skip(6);
                charId = reader.ReadUInt32();
            }

            var character = client.User.Characters.Single(x => x.ID == charId);

            client.Log($"Player logged in with char {character.Name}");
        }
    }
}
