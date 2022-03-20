using System.Threading.Tasks;
using Classic.World.Data;
using Classic.World.Packets.Server;

namespace Classic.World.Controllers;
public class AttackController
{
    private const int Cooldown = 1000;
    private const uint Damage = 10;

    private readonly WorldClient client;
    private bool isAttacking;
    private double remainingCooldown;

    // TODO: Reuse targetId from PlayerEntity?
    private Creature target;

    public AttackController(WorldClient client)
    {
        this.client = client;
    }

    public async Task StartAttacking(Creature unit)
    {
        if (this.isAttacking)
        {
            return;
        }

        this.isAttacking = true;
        this.target = unit;

        // TODO: Checks if can attack, range check etc.
        await this.client.SendPacket(new SMSG_ATTACKSTART(this.client.CharacterId, unit.ID));
    }

    public async Task Update(double dt)
    {
        if (!this.isAttacking)
        {
            // TODO: How to lower countdown, when player is not attacking,
            //       but still has an active cooldown? Setting to 0 would
            //       be stupid, as he then could cancel and attack without
            //       cooldowns.
            return;
        }

        if (this.target is null)
        {
            return; // Throw??
        }

        this.remainingCooldown -= dt;
        if (this.remainingCooldown > 0)
        {
            return;
        }

        await this.client.SendPacket(new SMSG_ATTACKERSTATEUPDATE(this.client.CharacterId, this.target.ID, Damage));
        this.target.Life -= Damage;
        this.remainingCooldown += Cooldown;
    }

    public async Task StopAttacking()
    {
        this.isAttacking = false;
        await this.client.SendPacket(new SMSG_ATTACKSTOP(this.client.CharacterId, this.target.ID));
    }
}

