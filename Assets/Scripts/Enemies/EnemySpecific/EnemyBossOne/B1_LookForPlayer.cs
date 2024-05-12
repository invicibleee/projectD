using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_LookForPlayer : LookForPlayerState
{
    private EnemyBossOne bossOne;
    public B1_LookForPlayer(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_LookForPlayer stateData, EnemyBossOne bossOne) : base(stateMashine, enemy, animBoolName, stateData)
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
        if (isPlayerInMaxAgroRange)
        {
            stateMashine.ChangeState(bossOne.rangeTargetAttackState);
        }
        else if (isPlayerInMinAgroRange)
        {
            if (Time.time >= startTime + bossOne.bossRollStateData.rollCooldown)
            {
                stateMashine.ChangeState(bossOne.rollState);
            }
            else
            {
                stateMashine.ChangeState(bossOne.idleState);
            }
        }
        else if (performCloseRangeAction)
        {
            stateMashine.ChangeState(bossOne.jumpAttackState);
        }
        if (isAllTurnsDone && !isPlayerInMaxAgroRange)
        {
            stateMashine.ChangeState(bossOne.rollState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
