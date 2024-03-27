using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_IdleState : IdleState
{
    private EnemyWolf enemyWolf;
    public E4_IdleState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_IdleState stateData, EnemyWolf enemyWolf) : base(stateMashine, enemy, animBoolName, stateData)
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
        if (isPlayerInMinAgroRange)
        {
            stateMashine.ChangeState(enemyWolf.playerDetectedState);
        }else if (isIdleTimeOver)
        {
            stateMashine.ChangeState(enemyWolf.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
