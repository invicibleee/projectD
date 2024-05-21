using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_MeleeAttackState : MeleeAttackState
{
    private EnemyBossTwo bossTwo;
    public B2_MeleeAttackState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, EnemyBossTwo bossTwo) : base(stateMashine, enemy, animBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
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

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
