using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E9_LookForPlayerState : LookForPlayerState
{
    private EnemyAsassin enemyAsassin;

    public E9_LookForPlayerState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_LookForPlayer stateData, EnemyAsassin enemyAsassin) : base(stateMashine, enemy, animBoolName, stateData)
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
        else if (isAllTurnsTimeDone)
        {
            stateMashine.ChangeState(enemyAsassin.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
