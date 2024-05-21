using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_ChargeState : ChargeState
{
    private EnemyBossTwo bossTwo;
    public B2_ChargeState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_ChargeState stateData, EnemyBossTwo bossTwo) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.bossTwo = bossTwo;
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
            stateMashine.ChangeState(bossTwo.meleeAttackState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMashine.ChangeState(bossTwo.lookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMashine.ChangeState(bossTwo.playerDetectedState);
            }
            else
            {
                stateMashine.ChangeState(bossTwo.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
