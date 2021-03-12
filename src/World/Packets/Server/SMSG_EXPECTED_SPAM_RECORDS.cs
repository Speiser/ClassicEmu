using System.Collections.Generic;
using System.Linq;

namespace Classic.World.Packets.Server
{
    public class SMSG_EXPECTED_SPAM_RECORDS : ServerPacketBase<Opcode>
    {
        private readonly IEnumerable<string> spamRecords;

        public SMSG_EXPECTED_SPAM_RECORDS(IEnumerable<string> spamRecords) : base(Opcode.SMSG_EXPECTED_SPAM_RECORDS)
        {
            this.spamRecords = spamRecords;
        }

        public override byte[] Get()
        {
            this.Writer.WriteUInt32((uint)this.spamRecords.Count());

            foreach (var spamRecord in this.spamRecords)
            {
                this.Writer.WriteString(spamRecord);
            }

            return this.Writer.Build();
        }
    }
}