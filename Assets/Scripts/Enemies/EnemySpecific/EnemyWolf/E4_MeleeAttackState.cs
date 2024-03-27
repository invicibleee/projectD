using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_MeleeAttackState : MeleeAttackState
{
    private EnemyWolf enemyWolf;
    public E4_MeleeAttackState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, EnemyWolf enemyWolf) : base(stateMashine, enemy, animBoolName, attackPosition, stateData)
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
                stateMashine.ChangeState(enemyWolf.playerDetectedState);
            }
            else
            {
                stateMashine.ChangeState(enemyWolf.lookForPlayerState);
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
