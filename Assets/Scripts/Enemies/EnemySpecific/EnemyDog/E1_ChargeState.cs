using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_ChargeState : ChargeState
{
    private EnemyDog enemy1;

    public E1_ChargeState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_ChargeState stateData, EnemyDog enemy1) : base(stateMashine, enemy1, animBoolName, stateData)
    {
        this.enemy1 = enemy1;
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
            stateMashine.ChangeState(enemy1.meleeAttackState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMashine.ChangeState(enemy1.lookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if(isPlayerInMinAgroRange)
            {
                stateMashine.ChangeState(enemy1.playerDetectedState);
            }
            else
            {
                stateMashine.ChangeState(enemy1.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
