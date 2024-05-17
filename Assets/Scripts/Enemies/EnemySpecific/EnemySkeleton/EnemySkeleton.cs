using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Enemy
{
    public E8_IdleState idleState { get; private set; }

    public E8_MoveState moveState { get; private set; }

    public E8_PlayerDetected playerDetectedState { get; private set; }

    public E8_ChargeState chageState { get; private set; }

    public E8_LookForPlayer lookForPlayerState { get; private set; }

    public E8_MeleeAttack meleeAttackState { get; private set; }

    public E8_DeathState deathState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_ChargeState chageStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_DeathState deathStateData;
    protected override void Start() 
    {
        base.Start();
        moveState = new E8_MoveState(stateMashine, this, "move", moveStateData, this);

        idleState = new E8_IdleState(stateMashine, this, "idle", idleStateData, this);

        playerDetectedState = new E8_PlayerDetected(stateMashine, this, "playerDetected", playerDetectedData, this);

        chageState = new E8_ChargeState(stateMashine, this, "charge", chageStateData, this);

        lookForPlayerState = new E8_LookForPlayer(stateMashine, this, "lookForPlayer", lookForPlayerStateData, this);

        meleeAttackState = new E8_MeleeAttack(stateMashine, this, "meleeAttack", attackCheck, meleeAttackStateData, this);

        deathState = new E8_DeathState(stateMashine,this,"death",deathStateData, this);

        stateMashine.Initialize(moveState);

    }



    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        DamageTaken();
    }

    protected void DamageTaken()
    {
        if (stats.currentHealth <= 0)
        {
            stateMashine.ChangeState(deathState);
        }
    }
}
