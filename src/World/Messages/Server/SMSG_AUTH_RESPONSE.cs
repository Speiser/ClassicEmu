using Classic.Shared.Data;

namespace Classic.World.Messages.Server
{
    public class SMSG_AUTH_RESPONSE : ServerMessageBase<Opcode>
    {
        private readonly int build;

        public SMSG_AUTH_RESPONSE(int build) : base(Opcode.SMSG_AUTH_RESPONSE)
        {
            this.build = build;
        }

        public override byte[] Get()
        {
            this.Writer
                .WriteUInt8(12)
                .WriteUInt32(0)
                .WriteUInt8(0)
                .WriteUInt32(0);

            switch (this.build)
            {
                case ClientBuild.TBC:
                    this.Writer.WriteUInt8(1);
                    break;
                case ClientBuild.WotLK:
                    this.Writer.WriteUInt8(2);
                    break;
            }

            return this.Writer.Build();
        }
    }
}
