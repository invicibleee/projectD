using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_MoveState : MoveState
{
    private EnemyArcher enemyArcher;
    public E2_MoveState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_MoveState stateData, EnemyArcher enemyArcher) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemyArcher = enemyArcher;
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


        if (isDetectingWall || !isDetectingLedge)
        {
            enemyArcher.idleState.SetFlipAfterIdle(true);
            stateMashine.ChangeState(enemyArcher.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
