using Classic.Common;

namespace Classic.World.Messages
{
    public class SMSG_BINDPOINTUPDATE : ServerMessageBase<Opcode>
    {
        public SMSG_BINDPOINTUPDATE() : base(Opcode.SMSG_BINDPOINTUPDATE)
        {
        }

        public override byte[] Get() => this.Writer
            .WriteFloat(-8919.284180F) // MapX
            .WriteFloat(-117.894028F) // MapY
            .WriteFloat(82.339821F) // MapZ
            .WriteUInt32(0) // MapID
            .WriteUInt32(12) // ZoneID
            .Build();
    }
}