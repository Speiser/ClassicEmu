using Classic.Common;
using System.Text;

namespace Classic.World.Messages
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
                .WriteUInt8(0x0A) // ChatMessageType.System
                .WriteUInt8(0)    // ChatMessageLanguage.Universal
                .WriteUInt64(this.characterId)
                .WriteUInt32((uint)this.message.Length + 1)
                .WriteBytes(Encoding.UTF8.GetBytes(message + '\0'))
                .WriteUInt8(0)
                .Build();
    }
}
