using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_PlayerDetectedState : PlayerDetectedState
{
    private EnemyArcher enemyArcher;
    public E2_PlayerDetectedState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_PlayerDetected stateData, EnemyArcher enemyArcher) : base(stateMashine, enemy, animBoolName, stateData)
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
        if (!isPlayerInMaxAgroRange)
        {
            stateMashine.ChangeState(enemyArcher.lookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            enemy.Flip();
            stateMashine.ChangeState(enemyArcher.moveState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
