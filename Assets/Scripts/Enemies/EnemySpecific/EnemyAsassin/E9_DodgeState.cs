using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E9_DodgeState : DodgeState
{
    private EnemyAsassin enemyAsassin;
    public E9_DodgeState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_DodgeState stateData, EnemyAsassin enemyAsassin) : base(stateMashine, enemy, animBoolName, stateData)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isDodgeOver)
        {
            if (isPLayerInMaxArgroRange && performCloseRangeAction)
            {
                stateMashine.ChangeState(enemyAsassin.meleeAttackState);
            }
            else if (!isPLayerInMaxArgroRange)
            {
                stateMashine.ChangeState(enemyAsassin.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
