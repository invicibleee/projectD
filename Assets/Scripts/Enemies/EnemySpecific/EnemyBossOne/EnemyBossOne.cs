using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyBossOne : Enemy
{
    public B1_IdleState idleState {  get; private set; }

    public B1_RollState rollState { get; private set; }

    public B1_RollOver rollOverState { get; private set; }

    public B1_RangeTargetAttackState rangeTargetAttackState { get; private set;}

    public B1_RangeTripleTargetAttackState rangeTripleTargetAttackState { get; private set; }

    public B1_LookForPlayer lookForPlayerState { get; private set; }

    public B1_JumpAttackState jumpAttackState { get; private set; }

    public B1_JumpAttackDone jumpAttackDone { get; private set; }

    public B1_DeathState deathState { get; private set; }

    [SerializeField] public D_BossRollState bossRollStateData;

    [SerializeField] private D_IdleState idleStateData;

    [SerializeField] private D_BossRollIsOverState bossRollIsOverStateData;

    [SerializeField] public D_RangeTargetAttackState rangeTargetAttackStateData;

    [SerializeField] public D_RangeTripleTargetAttackState rangeTripleTargetAttackStateData;

    [SerializeField] private D_LookForPlayer lookForPlayerStateData;

    [SerializeField] public D_JumpAttackState jumpAttackStateData;

    [SerializeField] private D_JumpAttackDone jumpAttackDoneData;

    [SerializeField] private Transform rangeAttackPosition;

    [SerializeField] private Transform rangeSecondAttackPosition;

    [SerializeField] private Transform rangeThirdAttackPosition;

    [SerializeField] private D_DeathState deathStateData;

    protected override void Start()
    {
        base.Start();
        idleState = new B1_IdleState(stateMashine,this,"idle",idleStateData,this);

        rollState = new B1_RollState(stateMashine,this,"roll",attackCheck,bossRollStateData,this);

        rollOverState = new B1_RollOver(stateMashine, this,"rollOver",bossRollIsOverStateData,this);

        rangeTargetAttackState = new B1_RangeTargetAttackState(stateMashine, this, "rangeAttack", rangeAttackPosition, rangeTargetAttackStateData, this);

        rangeTripleTargetAttackState = new B1_RangeTripleTargetAttackState(stateMashine, this,"rangeAttackX",rangeAttackPosition, rangeTripleTargetAttackStateData,rangeSecondAttackPosition, rangeThirdAttackPosition, this);

        lookForPlayerState = new B1_LookForPlayer(stateMashine,this, "lookForPlayer", lookForPlayerStateData,this);

        jumpAttackState = new B1_JumpAttackState(stateMashine, this, "jumpAttackState",  jumpAttackStateData, this);

        jumpAttackDone = new B1_JumpAttackDone(stateMashine, this,"jumpAttackDone",attackCheck,jumpAttackDoneData,this);

        deathState = new B1_DeathState(stateMashine,this,"death",deathStateData,this);

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
