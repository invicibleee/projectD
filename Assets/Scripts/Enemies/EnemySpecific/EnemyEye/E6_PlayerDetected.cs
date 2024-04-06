using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E6_PlayerDetected : PlayerDetectedState
{
    private EnemyEye enemyEye;
    public E6_PlayerDetected(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_PlayerDetected stateData, EnemyEye enemyEye) : base(stateMashine, enemy, animBoolName, stateData)
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
        if (performLongRangeAction)
        {
            stateMashine.ChangeState(enemyEye.rangeAttackState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMashine.ChangeState(enemyEye.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
