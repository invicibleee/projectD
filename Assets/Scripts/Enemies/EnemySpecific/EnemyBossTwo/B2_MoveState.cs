using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_MoveState : MoveState
{
    private EnemyBossTwo bossTwo;
    public B2_MoveState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_MoveState stateData, EnemyBossTwo bossTwo) : base(stateMashine, enemy, animBoolName, stateData)
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
        if (isPlayerInMinAgroRange)
        {
            stateMashine.ChangeState(bossTwo.playerDetectedState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            bossTwo.idleState.SetFlipAfterIdle(true);
            stateMashine.ChangeState(bossTwo.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
