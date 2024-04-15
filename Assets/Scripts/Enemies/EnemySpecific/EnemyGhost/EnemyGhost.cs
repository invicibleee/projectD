using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : Enemy
{
    public E7_IdleState idleState {  get; private set; }

    public E7_LookForPlayer lookForPlayerState { get; private set; }

    public E7_MeleeAtackState meleeAttackState { get; private set; }

    public E7_MoveState moveState { get; private set; }

    public E7_PlayerDetected playerDetectedState { get; private set; }

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

    protected override void Start()
    {
        base.Start();
        
        moveState = new E7_MoveState(stateMashine, this, "move", moveStateData, this);

        idleState = new E7_IdleState(stateMashine, this, "idle", idleStateData, this);

        lookForPlayerState = new E7_LookForPlayer(stateMashine, this, "lookForPlayer", lookForPlayerData, this);

        playerDetectedState = new E7_PlayerDetected(stateMashine,this,"playerDetected",playerDetectedData, this);

        meleeAttackState = new E7_MeleeAtackState(stateMashine, this, "meleeAttack", attackCheck, meleeAttackData, this);

        stateMashine.Initialize(moveState);




    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);

    }
}
