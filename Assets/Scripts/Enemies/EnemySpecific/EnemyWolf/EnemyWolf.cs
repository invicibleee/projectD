using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyWolf : Enemy
{
    public E4_IdleState idleState { get; private set; }

    public E4_MoveState moveState { get; private set; }

    public E4_PlayerDetectedState playerDetectedState { get; private set; }

    public E4_LookForPlayerState lookForPlayerState { get; private set; }

    public E4_ChargeState chargeState { get; private set; }

    public E4_MeleeAttackState meleeAttackState { get; private set;}

    public E4_StrongAttackState strongAttackState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackData;
    [SerializeField]
    private D_StrongAttackState strongAttackData;
    protected override void Start()
    {
        base.Start();

        idleState = new E4_IdleState(stateMashine, this, "idle", idleStateData, this);
        moveState = new E4_MoveState(stateMashine,this,"move",moveStateData, this);
        playerDetectedState = new E4_PlayerDetectedState(stateMashine, this, "playerDetected", playerDetectedData, this);
        lookForPlayerState = new E4_LookForPlayerState(stateMashine,this,"lookForPLayer",lookForPlayerData, this);
        chargeState = new E4_ChargeState(stateMashine, this, "charge", chargeStateData, this); 
        meleeAttackState = new E4_MeleeAttackState(stateMashine, this,"meleeAttack",attackCheck,meleeAttackData,this);
        strongAttackState = new E4_StrongAttackState(stateMashine, this, "strongMeleeAttack",attackCheck,strongAttackData,this);
        stateMashine.Initialize(moveState);

    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    protected override void Update()
    {
        base.Update();
        Debug.Log(stateMashine.currentState);

    }
}
