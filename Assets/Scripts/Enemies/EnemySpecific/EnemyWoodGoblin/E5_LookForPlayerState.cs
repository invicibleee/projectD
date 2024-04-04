using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E5_LookForPlayerState : LookForPlayerState
{
    private EnemyWoodGoblin enemyWoodGoblin;
    public E5_LookForPlayerState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_LookForPlayer stateData, EnemyWoodGoblin enemyWoodGoblin) : base(stateMashine, enemy, animBoolName, stateData)
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
        else if (isAllTurnsTimeDone)
        {
            stateMashine.ChangeState(enemyWoodGoblin.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
