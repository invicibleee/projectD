using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_RangeTripleTargetAttackState : RangeTripleTargetAttackState
{
    private EnemyBossOne bossOne;
    public B1_RangeTripleTargetAttackState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_RangeTripleTargetAttackState stateData, Transform secondAttackPosition, Transform thirdAttackPosition, EnemyBossOne bossOne) : base(stateMashine, enemy, animBoolName, attackPosition, stateData, secondAttackPosition, thirdAttackPosition)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (numberOfShots >= bossOne.rangeTripleTargetAttackState.countOfShots)
        {
            stateMashine.ChangeState(bossOne.rollState);
            numberOfShots = 0;

        }
        if (performCloseRangeAction || isPlayerInMinAgroRange)
        {
            if (Time.time >= bossOne.rollState.startTime + bossOne.bossRollStateData.rollCooldown)
            {
                stateMashine.ChangeState(bossOne.rollState);
            }
            else if (Time.time >= bossOne.jumpAttackState.startTime + bossOne.jumpAttackStateData.jumpAttackCooldown)
            {
                stateMashine.ChangeState(bossOne.jumpAttackState);
            }
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMashine.ChangeState(bossOne.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
