using Classic.Common;
using System;

namespace Classic.World.Messages
{
    public class SMSG_LOGIN_SETTIMESPEED : ServerMessageBase<Opcode>
    {
        public SMSG_LOGIN_SETTIMESPEED() : base(Opcode.SMSG_LOGIN_SETTIMESPEED)
        {
        }

        public override byte[] Get() => this.Writer
            .WriteUInt32(Convert.ToUInt32((DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds)) // TIME
            .WriteFloat(0.01666667f) // Speed
            .Build();
    }
}