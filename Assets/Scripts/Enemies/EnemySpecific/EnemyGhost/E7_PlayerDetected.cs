using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E7_PlayerDetected : PlayerDetectedState
{
    private EnemyGhost enemyGhost;

    public E7_PlayerDetected(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_PlayerDetected stateData, EnemyGhost enemyGhost) : base(stateMashine, enemy, animBoolName, stateData)
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
        if (performCloseRangeAction)
        {
            stateMashine.ChangeState(enemyGhost.meleeAttackState);
        }
        //else if (performLongRangeAction)
        //{
        //    //TODO : TeleportState
        //}
        else if (!isPlayerInMaxAgroRange)
        {
            stateMashine.ChangeState(enemyGhost.lookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            enemy.Flip();
            stateMashine.ChangeState(enemyGhost.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
