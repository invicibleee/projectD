using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E7_IdleState : IdleState
{
    private EnemyGhost enemyGhost;
    public E7_IdleState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_IdleState stateData, EnemyGhost enemyGhost) : base(stateMashine, enemy, animBoolName, stateData)
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
        else if (isIdleTimeOver)
        {
            stateMashine.ChangeState(enemyGhost.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
