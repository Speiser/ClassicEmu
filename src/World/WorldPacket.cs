namespace Classic.World
{
    public class WorldPacket
    {
        public WorldPacket(Opcode opcode, byte[] data)
        {
            this.Opcode = opcode;
            this.Data = data;
        }

        public Opcode Opcode { get; }
        public byte[] Header { get; private set; }
        public byte[] Data { get; }
    }
}
