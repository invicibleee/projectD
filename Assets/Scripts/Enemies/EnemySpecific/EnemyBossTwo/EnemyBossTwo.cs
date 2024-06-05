using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossTwo : Enemy
{
    protected Player player;

    public B2_ChargeState chargeState {  get; private set; }

    public B2_DeathState deathState { get; private set;}

    public B2_IdleState idleState { get; private set; }

    public B2_MoveState moveState { get; private set; }

    public B2_PlayerDetected playerDetectedState { get; private set; }

    public B2_MeleeAttackState meleeAttackState { get; private set; }

    public B2_LookForPlayerState lookForPlayerState { get; private set; }

    public B2_ShieldState shieldState { get; private set; }

    public B2_BackTeleportState backTeleportState { get; private set; }

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_DeathState deathStateData;
    [SerializeField] private D_PlayerDetected playerDetectedData;
    [SerializeField] private D_MeleeAttack meleeAttackData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_LookForPlayer lookForPlayerData;
    [SerializeField] public D_BossShieldState shieldStateData;
    [SerializeField] public D_BackTeleportState backTeleportStateData;
    protected override void Start()
    {
        base.Start();
        player = Transform.FindAnyObjectByType<Player>();


        chargeState = new B2_ChargeState(stateMashine, this, "charge", chargeStateData, this);

        idleState = new B2_IdleState(stateMashine,this, "idle", idleStateData, this);

        deathState = new B2_DeathState(stateMashine,this, "death", deathStateData, this);

        moveState = new B2_MoveState(stateMashine,this, "move", moveStateData, this);

        playerDetectedState = new B2_PlayerDetected(stateMashine,this, "playerDetected", playerDetectedData, this);

        lookForPlayerState = new B2_LookForPlayerState(stateMashine,this, "lookForPlayer", lookForPlayerData, this);

        meleeAttackState = new B2_MeleeAttackState(stateMashine,this, "meleeAttack", attackCheck,meleeAttackData,this);

        shieldState = new B2_ShieldState(stateMashine,this,"shield",shieldStateData, this);

        backTeleportState = new B2_BackTeleportState(stateMashine, this, "backTeleport", backTeleportStateData, player, this);

        stateMashine.Initialize(idleState);

    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        CheckDamageAndVisibility();
        if (stats.currentHealth <= 0)
        {
            stateMashine.ChangeState(deathState);
        }
    }

    private void CheckDamageAndVisibility()
    {
        if (stats.damaged && !CheckPlayerInMaxAgroRange())
        {
            stateMashine.ChangeState(lookForPlayerState);
            stats.damaged = false;
        }
    }
}
