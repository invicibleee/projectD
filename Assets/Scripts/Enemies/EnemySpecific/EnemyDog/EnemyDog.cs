using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDog : Enemy
{
    public E1_IdleState idleState { get; private set; }

    public E1_MoveState moveState { get; private set; }

    public E1_PlayerDetectedState playerDetectedState { get; private set; }

    public E1_ChargeState chageState { get; private set; }

    public E1_LookForPlayer lookForPlayerState { get; private set; }

    public E1_MeleeAttack meleeAttackState { get; private set; }
    
    public E1_DeathState deathState { get; private set; }

    public E1_StunState stunState { get; private set; }
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
    private D_StunState stunStateData;
    [SerializeField]
    private D_DeathState deathStateData;

    protected override void Start()
    {
        base.Start();
        moveState = new E1_MoveState(stateMashine, this, "move", moveStateData, this);

        idleState = new E1_IdleState(stateMashine, this, "idle", idleStateData, this);

        playerDetectedState = new E1_PlayerDetectedState(stateMashine, this,"playerDetected", playerDetectedData, this);

        chageState = new E1_ChargeState(stateMashine, this,"charge",chageStateData, this);

        lookForPlayerState = new E1_LookForPlayer(stateMashine, this, "lookForPlayer", lookForPlayerStateData, this);

        meleeAttackState = new E1_MeleeAttack(stateMashine, this, "meleeAttack", attackCheck, meleeAttackStateData, this);

        stunState = new E1_StunState(stateMashine, this, "stun", stunStateData, this);

        deathState = new E1_DeathState(stateMashine, this,"death", deathStateData, this);

        stateMashine.Initialize(moveState);
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);

    }

    public override void Damage()
    {
        base.Damage();
        if (isDead)
        {
            stateMashine.ChangeState(deathState);
        }
        else if (isStunned && stateMashine.currentState != stunState)
        {
            stateMashine.ChangeState(stunState);
        }

    }
}
