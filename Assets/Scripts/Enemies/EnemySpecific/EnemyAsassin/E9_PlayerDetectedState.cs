using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E9_PlayerDetectedState : PlayerDetectedState
{
    private EnemyAsassin enemyAsassin;

    public E9_PlayerDetectedState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_PlayerDetected stateData, EnemyAsassin enemyAsassin) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemyAsassin = enemyAsassin;
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
            if (Time.time >= enemyAsassin.dodgeState.startTime + enemyAsassin.dodgeStateData.dodgeCooldown)
            {
                stateMashine.ChangeState(enemyAsassin.dodgeState);
            }
            else
            {
                stateMashine.ChangeState(enemyAsassin.meleeAttackState);
            }
        }
        else if (performLongRangeAction)
        {
            if (Time.time >= enemyAsassin.backTeleportState.startTime + enemyAsassin.backTeleportStateData.teleportCooldown)
            {
                stateMashine.ChangeState(enemyAsassin.backTeleportState);
            }
            else
            {
                stateMashine.ChangeState(enemyAsassin.chargeState);

            }

        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMashine.ChangeState(enemyAsassin.lookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            enemy.Flip();
            stateMashine.ChangeState(enemyAsassin.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
