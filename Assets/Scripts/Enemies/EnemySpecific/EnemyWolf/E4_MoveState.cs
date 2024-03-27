using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_MoveState : MoveState
{
    private EnemyWolf enemyWolf;
    public E4_MoveState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_MoveState stateData, EnemyWolf enemyWolf) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemyWolf = enemyWolf;
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
            stateMashine.ChangeState(enemyWolf.playerDetectedState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            enemyWolf.idleState.SetFlipAfterIdle(true);
            stateMashine.ChangeState(enemyWolf.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
