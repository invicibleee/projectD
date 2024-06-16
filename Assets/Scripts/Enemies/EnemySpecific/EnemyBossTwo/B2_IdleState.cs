using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_IdleState : IdleState
{
    private EnemyBossTwo bossTwo;
    private bool playerDetected;
    public B2_IdleState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_IdleState stateData, EnemyBossTwo bossTwo) : base(stateMashine, enemy, animBoolName, stateData)
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
        playerDetected = false;

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
            playerDetected = true;
            stateMashine.ChangeState(bossTwo.playerDetectedState);
        }
        else if (isIdleTimeOver && playerDetected)
        {
            stateMashine.ChangeState(bossTwo.moveState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

