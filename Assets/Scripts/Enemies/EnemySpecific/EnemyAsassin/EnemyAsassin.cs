using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAsassin : Enemy
{
    public E9_IdleState idleState {  get; private set; }

    public E9_DodgeState dodgeState { get; private set; }

    public E9_LookForPlayerState lookForPlayerState { get; private set; }

    public E9_MeleeAttackState meleeAttackState { get; private set; }

    public E9_MoveState moveState { get; private set; }
    
    public E9_PlayerDetectedState playerDetectedState { get; private set; }

    public E9_ChargeState chargeState { get; private set; }

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
    public D_DodgeState dodgeStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;

    protected override void Start()
    {
        base.Start();

        moveState = new E9_MoveState(stateMashine, this, "move", moveStateData, this);

        idleState = new E9_IdleState(stateMashine, this, "idle", idleStateData, this);

        playerDetectedState = new E9_PlayerDetectedState(stateMashine, this, "playerDetected", playerDetectedData, this);

        meleeAttackState = new E9_MeleeAttackState(stateMashine, this, "meleeAttackOne", attackCheck, meleeAttackData, this);

        lookForPlayerState = new E9_LookForPlayerState(stateMashine, this, "lookForPlayer", lookForPlayerData, this);

        dodgeState = new E9_DodgeState(stateMashine, this, "dodge", dodgeStateData, this);

        chargeState = new E9_ChargeState(stateMashine, this,"charge",chargeStateData, this);

        stateMashine.Initialize(moveState);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);

    }
}
