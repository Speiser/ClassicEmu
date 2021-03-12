using Classic.World.Data;

namespace Classic.World.Packets.Server
{
    public class SMSG_ACTION_BUTTONS : ServerPacketBase<Opcode>
    {
        private readonly ActionBarItem[] actionBar;

        public SMSG_ACTION_BUTTONS(ActionBarItem[] actionBar) : base(Opcode.SMSG_ACTION_BUTTONS)
        {
            this.actionBar = actionBar;
        }

        public override byte[] Get()
        {
            for (var i = 0; i < actionBar.Length; i++)
            {
                var item = actionBar[i];
                if (item != null)
                    this.Writer.WriteUInt32((uint)item.SpellId | ((uint)item.Type << 24));
                else
                    this.Writer.WriteUInt32(0);
            }

            return this.Writer.Build();
        }
    }
}
