using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E6_MoveState : MoveState
{
    private EnemyEye enemyEye;
    public E6_MoveState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_MoveState stateData, EnemyEye enemyEye) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemyEye = enemyEye;
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
            stateMashine.ChangeState(enemyEye.rangeAttackState);

        }
        else if (isPlayerInMinAgroRange)
        {
            stateMashine.ChangeState(enemyEye.playerDetectedState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            enemyEye.idleState.SetFlipAfterIdle(true);
            stateMashine.ChangeState(enemyEye.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
