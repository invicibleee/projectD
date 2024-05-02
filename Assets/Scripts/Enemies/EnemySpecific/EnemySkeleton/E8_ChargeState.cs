using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E8_ChargeState : ChargeState
{
    private EnemySkeleton enemySkeleton;
    public E8_ChargeState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_ChargeState stateData, EnemySkeleton enemySkeleton) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemySkeleton = enemySkeleton;
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
            stateMashine.ChangeState(enemySkeleton.meleeAttackState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMashine.ChangeState(enemySkeleton.lookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMashine.ChangeState(enemySkeleton.playerDetectedState);
            }
            else
            {
                stateMashine.ChangeState(enemySkeleton.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
