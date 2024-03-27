using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_ChargeState : ChargeState
{
    private EnemyWolf enemyWolf;
    public E4_ChargeState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_ChargeState stateData, EnemyWolf enemyWolf) : base(stateMashine, enemy, animBoolName, stateData)
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
        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMashine.ChangeState(enemyWolf.lookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMashine.ChangeState(enemyWolf.playerDetectedState);
            }
            else
            {
                stateMashine.ChangeState(enemyWolf.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
