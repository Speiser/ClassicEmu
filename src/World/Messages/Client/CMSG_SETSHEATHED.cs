using Classic.Shared;

namespace Classic.World.Messages.Client
{
    public class CMSG_SETSHEATHED
    {
        public CMSG_SETSHEATHED(byte[] data)
        {
            using var reader = new PacketReader(data);
            this.Sheated = (SheathState)reader.ReadUInt32();
        }

        public SheathState Sheated { get; }

        public enum SheathState
        {
            SHEATH_STATE_UNARMED = 0, // non prepared weapon
            SHEATH_STATE_MELEE = 1,   // prepared melee weapon
            SHEATH_STATE_RANGED = 2   // prepared ranged weapon
        }
    }
}
