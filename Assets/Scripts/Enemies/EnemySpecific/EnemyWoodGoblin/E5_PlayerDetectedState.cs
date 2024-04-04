using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E5_PlayerDetectedState : PlayerDetectedState
{
    private EnemyWoodGoblin enemyWoodGoblin;
    public E5_PlayerDetectedState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_PlayerDetected stateData, EnemyWoodGoblin enemyWoodGoblin) : base(stateMashine, enemy, animBoolName, stateData)
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
        if (performLongRangeAction)
        {
            stateMashine.ChangeState(enemyWoodGoblin.rangeAttackState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMashine.ChangeState(enemyWoodGoblin.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
