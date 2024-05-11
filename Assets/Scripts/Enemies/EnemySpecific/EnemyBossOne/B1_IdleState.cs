using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_IdleState : IdleState
{
    private EnemyBossOne bossOne;
    public B1_IdleState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_IdleState stateData, EnemyBossOne bossOne) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.bossOne = bossOne;
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
           stateMashine.ChangeState(bossOne.rollState);
        }
        else if (isPlayerInMaxAgroRange)
        {
            stateMashine.ChangeState(bossOne.rangeTargetAttackState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
