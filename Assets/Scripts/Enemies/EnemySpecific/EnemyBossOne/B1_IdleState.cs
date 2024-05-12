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
            stateMashine.ChangeState(bossOne.rangeTargetAttackState);
        }
        else if (performCloseRangeAction)
        {
            playerDetected = true;
            stateMashine.ChangeState(bossOne.jumpAttackState);
        }
        else if(isPlayerInMinAgroRange)
        {
            playerDetected = true;
            if (Time.time >= bossOne.rollState.startTime + bossOne.bossRollStateData.rollCooldown)
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
