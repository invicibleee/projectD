using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_MoveState : MoveState
{
    private EnemySpider enemySpider;
    public E3_MoveState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_MoveState stateData, EnemySpider enemySpider) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemySpider = enemySpider;
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
        if(isPlayerInMinAgroRange)
        {
            stateMashine.ChangeState(enemySpider.playerDetectedState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            enemySpider.idleState.SetFlipAfterIdle(true);
            stateMashine.ChangeState(enemySpider.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
