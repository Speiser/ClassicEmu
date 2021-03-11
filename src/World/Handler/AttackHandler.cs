using System.Linq;
using System.Threading.Tasks;
using Classic.World.Messages;
using Classic.World.Messages.Client;
using Classic.World.Messages.Server;
using Microsoft.Extensions.Logging;

namespace Classic.World.Handler
{
    public class AttackHandler
    {
        [OpcodeHandler(Opcode.CMSG_SET_SELECTION)]
        public static Task OnSetSelection(PacketHandlerContext c)
        {
            var request = new CMSG_SET_SELECTION(c.Data);
            c.Client.Player.TargetId = request.TargetId;
            return Task.CompletedTask;
        }

        [OpcodeHandler(Opcode.CMSG_ATTACKSWING)]
        public static async Task OnAttackSwing(PacketHandlerContext c)
        {
            var request = new CMSG_ATTACKSWING(c.Data);

            // TODO: Also check players
            var unit = c.WorldState.Creatures.SingleOrDefault(creature => creature.ID == request.Guid);

            if (unit is null)
            {
                c.Client.Log($"Could not find unit: {request.Guid}");
                return;
            }

            // TODO: Checks if can attack, range check etc.
            await c.Client.SendPacket(new SMSG_ATTACKSTART(c.Client.Character.Id, unit.ID));

            // TODO: Do this somewhere else (Add AttackController to Player??)
            _ = Task.Run(async () =>
            {
                c.Client.Log("Attacking...", LogLevel.Information);
                for (var i = 0; i < 500; i++)
                {
                    await Task.Delay(500);
                    await c.Client.SendPacket(new SMSG_ATTACKERSTATEUPDATE(c.Client.Character.Id, unit.ID));
                }
            });
        }

        [OpcodeHandler(Opcode.CMSG_SETSHEATHED)]
        public static async Task OnSetSheathed(PacketHandlerContext c)
        {
            var request = new CMSG_SETSHEATHED(c.Data);
            c.Client.Log(request.Sheated.ToString());
        }

        [OpcodeHandler(Opcode.CMSG_ATTACKSTOP)]
        public static async Task OnAttackStop(PacketHandlerContext c)
        {
            // AttackController.Stop???
        }

        [OpcodeHandler(Opcode.CMSG_CAST_SPELL)]
        public static async Task OnCastSpell(PacketHandlerContext c)
        {
            var request = new CMSG_CAST_SPELL(c.Data);
            c.Client.Log($"Casting: {request.SpellId}");

            var unit = c.WorldState.Creatures.SingleOrDefault(creature => creature.ID == request.TargetId);

            if (unit is null)
            {
                c.Client.Log($"Could not find unit: {request.TargetId}");
                return;
            }
            else
            {
                c.Client.Log($"{unit.ID} - {unit.Model}");
            }

            // ????
            //await args.Client.SendPacket(SMSG_CAST_RESULT.Success(request.SpellId));

            // Opcode.SMSG_SPELLLOGEXECUTE
            //m_spellLogData.Initialize(SMSG_SPELLLOGEXECUTE);
            //m_spellLogData << m_spell->GetCaster()->GetPackGUID();
            //m_spellLogData << uint32(m_spell->m_spellInfo->Id);
            //m_spellLogDataEffectsCounterPos = m_spellLogData.wpos();
            //m_spellLogData << uint32(0);                            //placeholder
            //m_spellLogDataEffectsCounter = 0;
        }
    }
}
