using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MeleeAttack : MeleeAttackState
{
    Enemy1 enemy;
    public E1_MeleeAttack(FiniteStateMashine stateMashine, Entity entity, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, Enemy1 enemy) : base(stateMashine, entity, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
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
                stateMashine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                stateMashine.ChangeState(enemy.lookForPlayerState);
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
