namespace Classic.World.Packets.Server
{
    public class SMSG_SET_REST_START : ServerPacketBase<Opcode>
    {
        public SMSG_SET_REST_START() : base(Opcode.SMSG_SET_REST_START)
        {
        }

        public override byte[] Get() => this.Writer.WriteUInt32(0).Build(); // TODO VANILLA VS TBC
    }
}