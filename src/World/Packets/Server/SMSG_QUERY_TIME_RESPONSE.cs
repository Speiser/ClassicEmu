using System;

namespace Classic.World.Packets.Server;

public class SMSG_QUERY_TIME_RESPONSE : ServerPacketBase<Opcode>
{
    public SMSG_QUERY_TIME_RESPONSE() : base(Opcode.SMSG_QUERY_TIME_RESPONSE)
    {
    }

    public override byte[] Get()
    {
        var time = DateTime.Now - new DateTime(1970, 1, 1);
        return Writer.WriteUInt32((uint)time.TotalSeconds).Build();
    }
}
