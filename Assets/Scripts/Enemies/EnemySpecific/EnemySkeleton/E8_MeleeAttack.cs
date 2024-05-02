using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E8_MeleeAttack : MeleeAttackState
{
    private EnemySkeleton enemySkeleton;
    public E8_MeleeAttack(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, EnemySkeleton enemySkeleton) : base(stateMashine, enemy, animBoolName, attackPosition, stateData)
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
                stateMashine.ChangeState(enemySkeleton.playerDetectedState);
            }
            else
            {
                stateMashine.ChangeState(enemySkeleton.lookForPlayerState);
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

