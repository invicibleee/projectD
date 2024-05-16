using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemySpider : Enemy
{
    public E3_IdleState idleState { get; private set; }

    public E3_MoveState moveState { get; private set; }

    public E3_LookForPlayerState lookForPlayerState { get; private set;}

    public E3_PlayerDetectedState playerDetectedState { get; private set; } 

    public E3_MeeleAttack meleeAttackState { get; private set; }

    public E3_ChargeState chargeState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    protected override void Start()
    {
        base.Start();
        
        moveState = new E3_MoveState(stateMashine, this, "move", moveStateData, this);
        
        idleState = new E3_IdleState(stateMashine, this, "idle", idleStateData, this);
        
        playerDetectedState = new E3_PlayerDetectedState(stateMashine, this, "playerDetected", playerDetectedData, this);
        
        lookForPlayerState = new E3_LookForPlayerState(stateMashine, this, "lookForPlayer", lookForPlayerStateData, this);
        
        meleeAttackState = new E3_MeeleAttack(stateMashine, this, "meeleAttack",attackCheck, meleeAttackStateData, this);

        chargeState = new E3_ChargeState(stateMashine, this, "charge", chargeStateData, this); 

        stateMashine.Initialize(moveState);


    }




    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);

    }

    //public override void Damage()
    //{
    //    base.Damage();

    //    //if (isDead)
    //    //{
    //    //    stateMashine.ChangeState(deathState);
    //    //}
    //    //else if (isStunned && stateMashine.currentState != stunState)
    //    //{
    //    //    stateMashine.ChangeState(stunState);
    //    //}


    //}
}
