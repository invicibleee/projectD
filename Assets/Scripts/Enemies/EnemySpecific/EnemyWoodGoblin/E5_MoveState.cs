using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class E5_MoveState : MoveState
{
    private EnemyWoodGoblin enemyWoodGoblin;
    public E5_MoveState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_MoveState stateData, EnemyWoodGoblin enemyWoodGoblin) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemyWoodGoblin = enemyWoodGoblin;
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
        if (isPlayerInMaxAgroRange)
        {
            stateMashine.ChangeState(enemyWoodGoblin.rangeAttackState);

        }
        else if (isPlayerInMinAgroRange)
        {
            stateMashine.ChangeState(enemyWoodGoblin.playerDetectedState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            enemyWoodGoblin.idleState.SetFlipAfterIdle(true);
            stateMashine.ChangeState(enemyWoodGoblin.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
