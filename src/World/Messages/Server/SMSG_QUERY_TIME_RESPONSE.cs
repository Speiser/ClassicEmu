using System;
using Classic.Common;

namespace Classic.World.Messages.Server
{
    public class SMSG_QUERY_TIME_RESPONSE : ServerMessageBase<Opcode>
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
}
