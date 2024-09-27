using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Classic.World.Extensions;
using Classic.World.Packets;
using Classic.World.Packets.Client;
using Classic.World.Packets.Server;

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
            // TODO: Could still be a cast without a target
            return;
        }
        else
        {
            c.Client.Log($"{unit.ID} - {unit.Model}");
        }

        await c.Client.SendPacket(new SMSG_SPELL_START(c.Client.CharacterId, unit.ID, request.SpellId, 4000));

        // Simulate spell execution (instant cast)
        await Task.Delay(4000);  // Simulate a slight delay before spell hits

        // Send spell go (spell finished)
        await c.Client.SendPacket(new SMSG_SPELL_GO(c.Client.CharacterId, unit.ID, request.SpellId));

        await c.Client.SendPacket(SMSG_CAST_RESULT.Success(request.SpellId));

        unit.Life -= 100;

        var spellLog = new SMSG_SPELLLOGEXECUTE(c.Client.CharacterId, request.SpellId, SpellLogType.Damage,
        [
            new()
            {
                TargetGuid = unit.ID,
                EffectData = 100,
            }
        ]);

        // TODO: Send to all clients in player's range
        await c.Client.SendPacket(spellLog);
    }
}
