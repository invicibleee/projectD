using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E9_MoveState : MoveState
{
    private EnemyAsassin enemyAsassin;
    public E9_MoveState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_MoveState stateData, EnemyAsassin enemyAsassin) : base(stateMashine, enemy, animBoolName, stateData)
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
        if (isPlayerInMinAgroRange)
        {
            stateMashine.ChangeState(enemyAsassin.playerDetectedState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            enemyAsassin.idleState.SetFlipAfterIdle(true);
            stateMashine.ChangeState(enemyAsassin.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
