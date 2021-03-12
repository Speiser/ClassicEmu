using System;

namespace Classic.World.Packets.Server
{
    public class SMSG_LOGIN_SETTIMESPEED : ServerPacketBase<Opcode>
    {
        public SMSG_LOGIN_SETTIMESPEED() : base(Opcode.SMSG_LOGIN_SETTIMESPEED)
        {
        }

        public override byte[] Get() => this.Writer
            .WriteUInt32(CalculateCurrentTime()) // TIME
            .WriteFloat(0.01666667f) // Speed
            .Build();

        private static uint CalculateCurrentTime()
        {
            var time = DateTime.Now;
            var year = time.Year - 2000;
            var month = time.Month - 1;
            var day = time.Day - 1;

            return (uint)(time.Minute | (time.Hour << 6) | ((int)time.DayOfWeek << 11) | (day << 14) | (month << 20) | (year << 24));
        }
    }
}