using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_PlayerDetectedState : PlayerDetectedState
{
    private EnemyArcher enemyArcher;
    public E2_PlayerDetectedState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_PlayerDetected stateData, EnemyArcher enemyArcher) : base(stateMashine, enemy, animBoolName, stateData)
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
        if(performCloseRangeAction)
        {
            if(Time.time >= enemyArcher.dodgeState.startTime + enemyArcher.dodgeStateData.dodgeCooldown)
            {
                stateMashine.ChangeState(enemyArcher.dodgeState);
            }
            else
            {
                stateMashine.ChangeState(enemyArcher.meleeAttackState);
            }            
        }
        else if (performLongRangeAction)
        {
            stateMashine.ChangeState(enemyArcher.rangeAttackState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMashine.ChangeState(enemyArcher.lookForPlayerState);
        }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
