using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E6_LookForPlayer : LookForPlayerState
{
    private EnemyEye enemyEye;
    public E6_LookForPlayer(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_LookForPlayer stateData, EnemyEye enemyEye) : base(stateMashine, enemy, animBoolName, stateData)
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
        if (isPlayerInMinAgroRange)
        {
            stateMashine.ChangeState(enemyEye.playerDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateMashine.ChangeState(enemyEye.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
