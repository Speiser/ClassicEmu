using Classic.Common;

namespace Classic.World.Messages
{
    public class SMSG_INIT_WORLD_STATES : ServerMessageBase<Opcode>
    {
        public SMSG_INIT_WORLD_STATES() : base(Opcode.SMSG_INIT_WORLD_STATES)
        {
        }

        // https://www.ownedcore.com/forums/world-of-warcraft/world-of-warcraft-emulator-servers/wow-emu-questions-requests/327009-making-capturable-pvp-zones.html
        public override byte[] Get() => this.Writer
            .WriteUInt64(0) // MapID
            .WriteUInt32(12) // MapZone (ZoneID??)
            .WriteUInt32(0) // AreaID
            .WriteUInt16(12) // count of uint64 blocks
            .WriteUInt64(0x8d8)
            .WriteUInt64(0x0)
            .WriteUInt64(0x8d7)
            .WriteUInt64(0x0)
            .WriteUInt64(0x8d6)
            .WriteUInt64(0x0)
            .WriteUInt64(0x8d5)
            .WriteUInt64(0x0)
            .WriteUInt64(0x8d4)
            .WriteUInt64(0x0)
            .WriteUInt64(0x8d3)
            .WriteUInt64(0x0)
            .Build();
    }
}