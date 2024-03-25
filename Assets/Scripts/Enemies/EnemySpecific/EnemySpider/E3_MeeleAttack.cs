using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_MeeleAttack : MeleeAttackState
{
    private EnemySpider enemySpider;
    public E3_MeeleAttack(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, EnemySpider enemySpider) : base(stateMashine, enemy, animBoolName, attackPosition, stateData)
    {
        this.enemySpider = enemySpider;
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
                stateMashine.ChangeState(enemySpider.playerDetectedState);
            }
            else
            {
                stateMashine.ChangeState(enemySpider.lookForPlayerState);
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
