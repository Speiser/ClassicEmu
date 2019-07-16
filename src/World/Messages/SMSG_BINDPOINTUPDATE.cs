using Classic.Common;
using Classic.Data;

namespace Classic.World.Messages
{
    public class SMSG_BINDPOINTUPDATE : ServerMessageBase<Opcode>
    {
        public SMSG_BINDPOINTUPDATE() : base(Opcode.SMSG_BINDPOINTUPDATE)
        {
        }

        public override byte[] Get() => this.Writer
            .WriteFloat(Map.Default.X) // MapX
            .WriteFloat(Map.Default.Y) // MapY
            .WriteFloat(Map.Default.Z) // MapZ
            .WriteUInt32((uint)Map.Default.ID) // MapID
            .WriteUInt32((uint)Map.Default.Zone) // ZoneID
            .Build();
    }
}