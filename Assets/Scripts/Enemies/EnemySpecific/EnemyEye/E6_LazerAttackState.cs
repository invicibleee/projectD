using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E6_LazerAttackState : LazerAttackState
{
    private EnemyEye enemyEye;
    public E6_LazerAttackState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_LazerAttackState stateData, EnemyEye enemyEye) : base(stateMashine, enemy, animBoolName, attackPosition, stateData)
    {
        this.enemyEye = enemyEye;
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
                stateMashine.ChangeState(enemyEye.playerDetectedState);
            }
            else
            {
                stateMashine.ChangeState(enemyEye.lookForPlayerState);
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
