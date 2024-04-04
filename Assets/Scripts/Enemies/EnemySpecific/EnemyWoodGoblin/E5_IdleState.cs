using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E5_IdleState : IdleState
{
    private EnemyWoodGoblin enemyWoodGoblin;
    public E5_IdleState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_IdleState stateData, EnemyWoodGoblin enemyWoodGoblin) : base(stateMashine, enemy, animBoolName, stateData)
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
        if (isPlayerInMinAgroRange)
        {
            stateMashine.ChangeState(enemyWoodGoblin.playerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            stateMashine.ChangeState(enemyWoodGoblin.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
