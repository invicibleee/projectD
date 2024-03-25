using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_DodgeState : DodgeState
{
    private EnemyArcher enemyArcher;
    public E2_DodgeState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_DodgeState stateData, EnemyArcher enemyArcher) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemyArcher = enemyArcher;
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isDodgeOver)
        {
            if (isPLayerInMaxArgroRange && performCloseRangeAction)
            {
                stateMashine.ChangeState(enemyArcher.meleeAttackState);
            }
            else if (isPLayerInMaxArgroRange && !performCloseRangeAction)
            {
                stateMashine.ChangeState(enemyArcher.rangeAttackState);
            }
            else if (!isPLayerInMaxArgroRange)
            {
                stateMashine.ChangeState(enemyArcher.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
