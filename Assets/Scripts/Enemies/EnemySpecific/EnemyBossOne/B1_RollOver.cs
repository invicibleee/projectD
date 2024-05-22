using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_RollOver : BossRollIsOverState
{
    private EnemyBossOne bossOne;
    public B1_RollOver(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_BossRollIsOverState stateData, EnemyBossOne bossOne) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.bossOne = bossOne;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isRollTimeDone)
        {
            if (performCloseRangeAction || isPlayerInMaxAgroRange)
            {
                if (Time.time >= bossOne.jumpAttackState.startTime + bossOne.jumpAttackStateData.jumpAttackCooldown && bossOne.stats.currentHealth <= bossOne.stats.maxHealth.GetValue() / 2)
                {
                    stateMashine.ChangeState(bossOne.jumpAttackState);
                }
                else if (Time.time >= bossOne.rangeTargetAttackState.startTime + bossOne.rangeTargetAttackStateData.rangeAttackCooldown && bossOne.stats.currentHealth >= bossOne.stats.maxHealth.GetValue() / 2)
                {
                    stateMashine.ChangeState(bossOne.rangeTargetAttackState);
                }
                else if (Time.time >= bossOne.rangeTripleTargetAttackState.startTime + bossOne.rangeTripleTargetAttackStateData.rangeTripleAttackCooldown && bossOne.stats.currentHealth <= bossOne.stats.maxHealth.GetValue() / 2)
                {
                    stateMashine.ChangeState(bossOne.rangeTripleTargetAttackState);
                }else
                {
                    stateMashine.ChangeState(bossOne.idleState);
                }
            }
            else 
            {
                stateMashine.ChangeState(bossOne.lookForPlayerState);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
