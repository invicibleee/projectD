using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_PlayerDetected : PlayerDetectedState
{
    private EnemyBossTwo bossTwo;
    public B2_PlayerDetected(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_PlayerDetected stateData, EnemyBossTwo bossTwo) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.bossTwo = bossTwo;
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
        if (performCloseRangeAction)
        {
            if(Time.time >= bossTwo.shieldState.startTime + bossTwo.shieldStateData.shieldCooldown && bossTwo.stats.damaged)
            {
                stateMashine.ChangeState(bossTwo.shieldState);
            }
            else
            {
                stateMashine.ChangeState(bossTwo.meleeAttackState);
            }
        }
        else if (performLongRangeAction)
        {
            if (Time.time >= bossTwo.backTeleportState.startTime + bossTwo.backTeleportStateData.teleportCooldown)
            {
                stateMashine.ChangeState(bossTwo.backTeleportState);
            }
            else
            {
                stateMashine.ChangeState(bossTwo.chargeState);

            }
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMashine.ChangeState(bossTwo.lookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            enemy.Flip();
            stateMashine.ChangeState(bossTwo.moveState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
