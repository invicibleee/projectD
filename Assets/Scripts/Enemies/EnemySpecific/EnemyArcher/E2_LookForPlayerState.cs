using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_LookForPlayerState : LookForPlayerState
{
    private EnemyArcher enemyArcher;
    public E2_LookForPlayerState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_LookForPlayer stateData, EnemyArcher enemyArcher) : base(stateMashine, enemy, animBoolName, stateData)
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
        if (isPlayerInMinAgroRange)
        {
            stateMashine.ChangeState(enemyArcher.playerDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateMashine.ChangeState(enemyArcher.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
