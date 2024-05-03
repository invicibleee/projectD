using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E9_ChargeState : ChargeState
{
    private EnemyAsassin enemyAsassin;
    public E9_ChargeState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_ChargeState stateData, EnemyAsassin enemyAsassin) : base(stateMashine, enemy, animBoolName, stateData)
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
            stateMashine.ChangeState(enemyAsassin.meleeAttackState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMashine.ChangeState(enemyAsassin.lookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMashine.ChangeState(enemyAsassin.playerDetectedState);
            }
            else
            {
                stateMashine.ChangeState(enemyAsassin.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
