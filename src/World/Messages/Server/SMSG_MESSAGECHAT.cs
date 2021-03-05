using System.Text;
using Classic.World.Data.Enums.Chat;

namespace Classic.World.Messages.Server
{
    public class SMSG_MESSAGECHAT : ServerMessageBase<Opcode>
    {
        private readonly ulong characterId;
        private readonly string message;

        public SMSG_MESSAGECHAT(ulong characterId, string message) : base(Opcode.SMSG_MESSAGECHAT)
        {
            this.characterId = characterId;
            this.message = message;
        }

        public override byte[] Get()
            => this.Writer
                .WriteUInt8((byte)MessageType.System)
                .WriteUInt32((uint)MessageLanguage.Universal)
                .WriteUInt64(this.characterId)
                .WriteUInt32((uint)this.message.Length + 1)
                .WriteBytes(Encoding.UTF8.GetBytes(message + '\n'))
                .WriteUInt8(0) // chatTag ??
                .Build();
    }
}
