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

    public AttackController(WorldClient client)
    {
        this.client = client;
    }

    private Creature Target => this.client.Player.Target;

    public async Task StartAttacking()
    {
        if (this.isAttacking)
        {
            return;
        }

        this.isAttacking = true;

        // TODO: Checks if can attack, range check etc.
        await this.client.SendPacket(new SMSG_ATTACKSTART(this.client.CharacterId, this.Target.ID));
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

        if (this.client.Player.Target is null)
        {
            return; // Throw??
        }

        this.remainingCooldown -= dt;
        if (this.remainingCooldown > 0)
        {
            return;
        }

        await this.client.SendPacket(new SMSG_ATTACKERSTATEUPDATE(this.client.CharacterId, this.Target.ID, Damage));
        this.Target.Life -= Damage;
        this.remainingCooldown += Cooldown;
    }

    public async Task StopAttacking()
    {
        this.isAttacking = false;
        await this.client.SendPacket(new SMSG_ATTACKSTOP(this.client.CharacterId, this.Target.ID));
    }
}

