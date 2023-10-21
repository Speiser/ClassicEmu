using System.Linq;
using System.Threading.Tasks;
using Classic.World.Extensions;
using Classic.World.Packets;
using Classic.World.Packets.Client;

namespace Classic.World.Handler;

public class AttackHandler
{
    [OpcodeHandler(Opcode.CMSG_SET_SELECTION)]
    public static Task OnSetSelection(PacketHandlerContext c)
    {
        var request = new CMSG_SET_SELECTION(c.Packet);
        c.TrySetTarget(request.TargetId);
        return Task.CompletedTask;
    }

    [OpcodeHandler(Opcode.CMSG_ATTACKSWING)]
    public static async Task OnAttackSwing(PacketHandlerContext c)
    {
        var request = new CMSG_ATTACKSWING(c.Packet);

        if (!c.TrySetTarget(request.Guid))
        {
            // return error?
            return;
        }

        await c.Client.AttackController.StartAttacking();
    }

    [OpcodeHandler(Opcode.CMSG_SETSHEATHED)]
    public static Task OnSetSheathed(PacketHandlerContext c)
    {
        var request = new CMSG_SETSHEATHED(c.Packet);
        c.Client.Player.SheathState = request.Sheated;
        return Task.CompletedTask;
    }

    [OpcodeHandler(Opcode.CMSG_ATTACKSTOP)]
    public static async Task OnAttackStop(PacketHandlerContext c)
    {
        await c.Client.AttackController.StopAttacking();
    }

    [OpcodeHandler(Opcode.CMSG_CAST_SPELL)]
    public static async Task OnCastSpell(PacketHandlerContext c)
    {
        var request = new CMSG_CAST_SPELL(c.Packet);
        c.Client.Log($"Casting: {request.SpellId}");

        var unit = c.World.Creatures.SingleOrDefault(creature => creature.ID == request.TargetId);

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
