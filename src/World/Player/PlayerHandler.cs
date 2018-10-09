using System.Linq;
using Classic.Common;
using DCharacter = Classic.Data.Character;

namespace Classic.World.Player
{
    public class PlayerHandler
    {
        private readonly WorldClient client;
        private readonly DCharacter character;

        public PlayerHandler(byte[] packet, WorldClient client)
        {
            this.client = client;
            uint charId = 0;

            using (var reader = new PacketReader(packet))
            {
                reader.Skip(6);
                charId = reader.ReadUInt32();
            }

            this.character = this.client.User.Characters.Single(x => x.ID == charId);

            this.Work();
        }

        private void Work()
        {
            this.client.Log($"Player logged in with char {this.character.Name}");
        }
    }
}
