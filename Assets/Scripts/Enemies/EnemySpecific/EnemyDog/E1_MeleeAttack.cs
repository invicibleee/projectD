using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MeleeAttack : MeleeAttackState
{
    EnemyDog enemy1;
    public E1_MeleeAttack(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, EnemyDog enemy1) : base(stateMashine, enemy1, animBoolName, attackPosition, stateData)
    {
        this.enemy1 = enemy1;
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
        if(isAnimationFinished)
        {
            if(isPlayerInMinAgroRange)
            {
                stateMashine.ChangeState(enemy1.playerDetectedState);
            }
            else
            {
                stateMashine.ChangeState(enemy1.lookForPlayerState);
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
