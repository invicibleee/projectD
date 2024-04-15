using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E7_MoveState : MoveState
{
    private EnemyGhost enemyGhost;
    public E7_MoveState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_MoveState stateData, EnemyGhost enemyGhost) : base(stateMashine, enemy, animBoolName, stateData)
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
        if (isPlayerInMinAgroRange)
        {
            stateMashine.ChangeState(enemyGhost.playerDetectedState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            enemyGhost.idleState.SetFlipAfterIdle(true);
            stateMashine.ChangeState(enemyGhost.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
