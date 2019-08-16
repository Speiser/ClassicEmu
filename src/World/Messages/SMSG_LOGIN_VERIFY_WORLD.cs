using Classic.Common;
using Classic.Data;

namespace Classic.World.Messages
{
    public class SMSG_LOGIN_VERIFY_WORLD : ServerMessageBase<Opcode>
    {
        public SMSG_LOGIN_VERIFY_WORLD() : base(Opcode.SMSG_LOGIN_VERIFY_WORLD)
        {
        }

        public override byte[] Get() => this.Writer
            .WriteInt32(Map.Default.ID) // MapID
            .WriteFloat(Map.Default.X) // MapX
            .WriteFloat(Map.Default.Y) // MapY
            .WriteFloat(Map.Default.Z) // MapZ
            .WriteFloat(Map.Default.Orientation) // MapO (Orientation)
            .Build();
    }
}