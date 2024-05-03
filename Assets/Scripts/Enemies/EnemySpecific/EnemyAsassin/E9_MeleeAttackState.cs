using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E9_MeleeAttackState : MeleeAttackState
{
    private EnemyAsassin enemyAsassin;
    public E9_MeleeAttackState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, EnemyAsassin enemyAsassin) : base(stateMashine, enemy, animBoolName, attackPosition, stateData)
    {
         this.enemyAsassin = enemyAsassin;
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
                stateMashine.ChangeState(enemyAsassin.playerDetectedState);
            }
            else
            {
                stateMashine.ChangeState(enemyAsassin.lookForPlayerState);
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
