using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_StunState : StunState
{
    private EnemyArcher enemyArcher;
    public E2_StunState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_StunState stateData, EnemyArcher enemyArcher) : base(stateMashine, enemy, animBoolName, stateData)
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
        if (isStunTimeOver)
        {
            if (preformCloseRangeAction)
            {
                stateMashine.ChangeState(enemyArcher.meleeAttackState);
            }
            else 
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
