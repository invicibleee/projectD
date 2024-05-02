using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E8_IdleState : IdleState
{
    private EnemySkeleton enemySkeleton;
    public E8_IdleState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_IdleState stateData, EnemySkeleton enemySkeleton) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemySkeleton = enemySkeleton;
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
            stateMashine.ChangeState(enemySkeleton.playerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            stateMashine.ChangeState(enemySkeleton.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
