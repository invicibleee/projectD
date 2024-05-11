using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossOne : Enemy
{
    public B1_IdleState idleState {  get; private set; }

    public B1_RollState rollState { get; private set; }

    public B1_RollOver rollOverState { get; private set; }

    public B1_RangeTargetAttackState rangeTargetAttackState { get; private set;}

    [SerializeField]
    public D_BossRollState bossRollStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_BossRollIsOverState bossRollIsOverStateData;
    [SerializeField]
    private D_RangeTargetAttackState rangeTargetAttackStateData;
    [SerializeField]
    private Transform rangeAttackPosition;
    protected override void Start()
    {
        base.Start();
        idleState = new B1_IdleState(stateMashine,this,"idle",idleStateData,this);

        rollState = new B1_RollState(stateMashine,this,"roll",attackCheck,bossRollStateData,this);

        rollOverState = new B1_RollOver(stateMashine, this,"rollOver",bossRollIsOverStateData,this);

        rangeTargetAttackState = new B1_RangeTargetAttackState(stateMashine, this, "rangeAttack", rangeAttackPosition, rangeTargetAttackStateData, this);

        stateMashine.Initialize(idleState);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);

    }
}
