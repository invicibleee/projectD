using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_PlayerDetectedState : PlayerDetectedState
{
    private EnemyWolf enemyWolf;
    public E4_PlayerDetectedState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_PlayerDetected stateData, EnemyWolf enemyWolf) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemyWolf = enemyWolf;
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
        if (performCloseRangeAction)
        {
            stateMashine.ChangeState(enemyWolf.meleeAttackState);
        }
        else if (performLongRangeAction)
        {
            stateMashine.ChangeState(enemyWolf.chargeState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMashine.ChangeState(enemyWolf.lookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            enemy.Flip();
            stateMashine.ChangeState(enemyWolf.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
