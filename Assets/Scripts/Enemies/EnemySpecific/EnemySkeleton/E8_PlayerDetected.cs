using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E8_PlayerDetected : PlayerDetectedState
{
    private EnemySkeleton enemySkeleton;
    public E8_PlayerDetected(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_PlayerDetected stateData, EnemySkeleton enemySkeleton) : base(stateMashine, enemy, animBoolName, stateData)
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
        else if (performLongRangeAction)
        {
            stateMashine.ChangeState(enemySkeleton.chageState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMashine.ChangeState(enemySkeleton.lookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            enemy.Flip();
            stateMashine.ChangeState(enemySkeleton.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
