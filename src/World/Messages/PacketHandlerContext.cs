namespace Classic.World.Messages
{
    public class PacketHandlerContext
    {
        public WorldClient Client { get; init; }
        public Opcode Opcode { get; init; }
        public byte[] Data { get; init; }
        public WorldState WorldState { get; init; }
    }
}
