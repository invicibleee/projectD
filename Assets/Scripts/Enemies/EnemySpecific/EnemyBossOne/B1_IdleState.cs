using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_IdleState : IdleState
{
    private EnemyBossOne bossOne;
    private bool playerDetected;
    public B1_IdleState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_IdleState stateData, EnemyBossOne bossOne) : base(stateMashine, enemy, animBoolName, stateData)
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
        playerDetected = false ;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMaxAgroRange)
        {
            playerDetected = true;
            if (Time.time >= bossOne.rangeTargetAttackState.startTime + bossOne.rangeTargetAttackStateData.rangeAttackCooldown && bossOne.stats.currentHealth >= bossOne.stats.maxHealth.GetValue() / 2)
            {
                stateMashine.ChangeState(bossOne.rangeTargetAttackState);
            }
            else if (Time.time >= bossOne.rangeTripleTargetAttackState.startTime + bossOne.rangeTripleTargetAttackStateData.rangeTripleAttackCooldown && bossOne.stats.currentHealth <= bossOne.stats.maxHealth.GetValue() /2)
            {
                stateMashine.ChangeState(bossOne.rangeTripleTargetAttackState);
            }

        }
        else if (performCloseRangeAction || isPlayerInMinAgroRange)
        {
            playerDetected = true;
            if (Time.time >= bossOne.jumpAttackState.startTime + bossOne.jumpAttackStateData.jumpAttackCooldown && bossOne.stats.currentHealth <= bossOne.stats.maxHealth.GetValue() / 2)
            {
                stateMashine.ChangeState(bossOne.jumpAttackState);
            }
            else if (Time.time >= bossOne.rollState.startTime + bossOne.bossRollStateData.rollCooldown)
            {
                stateMashine.ChangeState(bossOne.rollState);
            }
        }
        else if(playerDetected)
        {
            stateMashine.ChangeState(bossOne.lookForPlayerState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
