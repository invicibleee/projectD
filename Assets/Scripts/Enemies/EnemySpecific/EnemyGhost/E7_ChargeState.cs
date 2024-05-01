using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E7_ChargeState : ChargeState
{
    private EnemyGhost enemyGhost;
    public E7_ChargeState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_ChargeState stateData, EnemyGhost enemyGhost) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemyGhost = enemyGhost;
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
            stateMashine.ChangeState(enemyGhost.meleeAttackState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMashine.ChangeState(enemyGhost.lookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMashine.ChangeState(enemyGhost.playerDetectedState);
            }
            else
            {
                stateMashine.ChangeState(enemyGhost.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
