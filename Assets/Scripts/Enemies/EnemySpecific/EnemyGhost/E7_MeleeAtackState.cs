using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E7_MeleeAtackState : MeleeAttackState
{
    private EnemyGhost enemyGhost;
    public E7_MeleeAtackState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, EnemyGhost enemyGhost) : base(stateMashine, enemy, animBoolName, attackPosition, stateData)
    {
        this.enemyGhost = enemyGhost;
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
                stateMashine.ChangeState(enemyGhost.playerDetectedState);
            }
            else
            {
                stateMashine.ChangeState(enemyGhost.lookForPlayerState);
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
