using System.Linq;
using System.Threading.Tasks;
using Classic.World.Messages.Client;
using Classic.World.Messages.Server;
using Microsoft.Extensions.Logging;

namespace Classic.World.Handler
{
    public class AttackHandler
    {
        [OpcodeHandler(Opcode.CMSG_SET_SELECTION)]
        public static Task OnSetSelection(HandlerArguments args)
        {
            var request = new CMSG_SET_SELECTION(args.Data);
            args.Client.Player.TargetId = request.TargetId;
            return Task.CompletedTask;
        }

        [OpcodeHandler(Opcode.CMSG_ATTACKSWING)]
        public static async Task OnAttackSwing(HandlerArguments args)
        {
            var request = new CMSG_ATTACKSWING(args.Data);

            // TODO: Also check players
            var unit = args.WorldState.Creatures.SingleOrDefault(c => c.ID == request.Guid);

            if (unit is null)
            {
                args.Client.Log($"Could not find unit: {request.Guid}");
                return;
            }

            // TODO: Checks if can attack, range check etc.
            await args.Client.SendPacket(new SMSG_ATTACKSTART(args.Client.Character.ID, unit.ID));

            // TODO: Do this somewhere else (Add AttackController to Player??)
            _ = Task.Run(async () =>
            {
                args.Client.Log("Attacking...", LogLevel.Information);
                for (var i = 0; i < 500; i++)
                {
                    await Task.Delay(500);
                    await args.Client.SendPacket(new SMSG_ATTACKERSTATEUPDATE(args.Client.Character.ID, unit.ID));
                }
            });
        }

        [OpcodeHandler(Opcode.CMSG_SETSHEATHED)]
        public static async Task OnSetSheathed(HandlerArguments args)
        {
            var request = new CMSG_SETSHEATHED(args.Data);
            args.Client.Log(request.Sheated.ToString());
        }

        [OpcodeHandler(Opcode.CMSG_ATTACKSTOP)]
        public static async Task OnAttackStop(HandlerArguments args)
        {
            // AttackController.Stop???
        }

        [OpcodeHandler(Opcode.CMSG_CAST_SPELL)]
        public static async Task OnCastSpell(HandlerArguments args)
        {
            var request = new CMSG_CAST_SPELL(args.Data);
            args.Client.Log($"Casting: {request.SpellId}");

            var unit = args.WorldState.Creatures.SingleOrDefault(c => c.ID == request.TargetId);

            if (unit is null)
            {
                args.Client.Log($"Could not find unit: {request.TargetId}");
                return;
            }
            else
            {
                args.Client.Log($"{unit.ID} - {unit.Model}");
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
