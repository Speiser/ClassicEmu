namespace Classic.World.Packets.Server;

public class SMSG_CHAR_DELETE : ServerPacketBase<Opcode>
{
    private readonly byte status;

    public SMSG_CHAR_DELETE(byte status) : base(Opcode.SMSG_CHAR_DELETE)
    {
        this.status = status;
    }

    public override byte[] Get() => this.Writer.WriteUInt8(this.status).Build();
}
