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
        enemy.SetVelocityEnemy(0f);
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
            if (Time.time >= startTime + bossOne.bossRollStateData.rollCooldown)
            {
                stateMashine.ChangeState(bossOne.rollState);
            }
            else if (isPlayerInMaxAgroRange)
            {
                stateMashine.ChangeState(bossOne.rangeTargetAttackState);
            }
            else
            {
                stateMashine.ChangeState(bossOne.idleState);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
