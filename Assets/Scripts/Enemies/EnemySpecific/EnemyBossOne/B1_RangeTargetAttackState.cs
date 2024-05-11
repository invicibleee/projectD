using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_RangeTargetAttackState : RangeTargetAttackState
{
    private EnemyBossOne bossOne;
    public B1_RangeTargetAttackState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_RangeTargetAttackState stateData, EnemyBossOne bossOne) : base(stateMashine, enemy, animBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMinAgroRange)
        {
            stateMashine.ChangeState(bossOne.rollState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
