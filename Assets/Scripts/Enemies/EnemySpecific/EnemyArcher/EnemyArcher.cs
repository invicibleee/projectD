using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyArcher : Enemy
{
    public E2_IdleState idleState {  get; private set; }

    public E2_MoveState moveState { get; private set; }

    public E2_PlayerDetectedState playerDetectedState { get; private set; }

    public E2_LookForPlayerState lookForPlayerState { get; private set; }

    public E2_MeleeAttackState meleeAttackState { get; private set; }

    public E2_StunState stunState { get; private set; }

    public E2_DeathState deathState { get; private set; }

    public E2_DodgeState dodgeState { get; private set;}

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField] 
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerData;
    [SerializeField]
    private D_MeleeAttack meleeAttackData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField] 
    private D_DeathState deathStateData;
    [SerializeField]
    public D_DodgeState dodgeStateData;

    protected override void Start()
    {
        base.Start();
        moveState = new E2_MoveState(stateMashine, this, "move", moveStateData, this);

        idleState = new E2_IdleState(stateMashine, this, "idle", idleStateData, this);

        playerDetectedState = new E2_PlayerDetectedState(stateMashine, this, "playerDetected", playerDetectedData, this);

        meleeAttackState = new E2_MeleeAttackState(stateMashine, this, "meleeAttack", attackCheck, meleeAttackData, this);

        lookForPlayerState = new E2_LookForPlayerState(stateMashine, this,"lookForPlayer", lookForPlayerData, this);

        
        
        deathState = new E2_DeathState(stateMashine, this,"death",deathStateData, this);

        dodgeState = new E2_DodgeState(stateMashine, this,"dodge", dodgeStateData, this);

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
        }else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTunrImmediatly(true);
            stateMashine.ChangeState(lookForPlayerState);
        }

    }
}
