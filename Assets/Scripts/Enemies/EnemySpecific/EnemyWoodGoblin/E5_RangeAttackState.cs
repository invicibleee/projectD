using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E5_RangeAttackState : RangeAttackState
{
    private EnemyWoodGoblin enemyWoodGoblin;
    public E5_RangeAttackState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_RangeAttackState stateData, EnemyWoodGoblin enemyWoodGoblin) : base(stateMashine, enemy, animBoolName, attackPosition, stateData)
    {
        this.enemyWoodGoblin = enemyWoodGoblin;
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
                stateMashine.ChangeState(enemyWoodGoblin.playerDetectedState);
            }
            else
            {
                stateMashine.ChangeState(enemyWoodGoblin.lookForPlayerState);
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
