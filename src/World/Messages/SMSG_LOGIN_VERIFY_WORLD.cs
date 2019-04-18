using Classic.Common;

namespace Classic.World.Messages
{
    public class SMSG_LOGIN_VERIFY_WORLD : ServerMessageBase<Opcode>
    {
        public SMSG_LOGIN_VERIFY_WORLD() : base(Opcode.SMSG_LOGIN_VERIFY_WORLD)
        {
        }

        public override byte[] Get() => this.Writer
            // 0 -8919.284180 -117.894028 82.339821 = human starting area
            .WriteInt32(0) // MapID
            .WriteFloat(-8919.284180F) // MapX
            .WriteFloat(-117.894028F) // MapY
            .WriteFloat(82.339821F) // MapZ
            .WriteFloat(1F) // MapO (Orientation)
            .Build();
    }
}