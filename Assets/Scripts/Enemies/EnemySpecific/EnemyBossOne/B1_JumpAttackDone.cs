using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_JumpAttackDone : JumpAttackDone
{
    private EnemyBossOne bossOne;
    public B1_JumpAttackDone(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_JumpAttackDone stateData, EnemyBossOne bossOne) : base(stateMashine, enemy, animBoolName, attackPosition, stateData)
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

        if (isPlayerInMaxAgroRange)
        {
            if (Time.time >= bossOne.rangeTargetAttackState.startTime + bossOne.rangeTargetAttackStateData.rangeAttackCooldown)
            {
                stateMashine.ChangeState(bossOne.rangeTargetAttackState);
            }
            else if (Time.time >= bossOne.rangeTripleTargetAttackState.startTime + bossOne.rangeTripleTargetAttackStateData.rangeTripleAttackCooldown)
            {
                stateMashine.ChangeState(bossOne.rangeTripleTargetAttackState);
            }
        }
        if (isPlayerInMinAgroRange)
        {
            if (Time.time >= bossOne.rollState.startTime + bossOne.bossRollStateData.rollCooldown)
            {
                stateMashine.ChangeState(bossOne.rollState);
            }
        }
        else
        {
            stateMashine.ChangeState(bossOne.lookForPlayerState);
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
