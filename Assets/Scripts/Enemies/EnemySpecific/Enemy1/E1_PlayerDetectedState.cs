using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy1;
    public E1_PlayerDetectedState(FiniteStateMashine stateMashine, Enemy enemy, string animBoolName, D_PlayerDetected stateData, Enemy1 enemy1) : base(stateMashine, enemy1, animBoolName, stateData)
    {
        this.enemy1 = enemy1;
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
        if (performCloseRangeAction)
        {
            stateMashine.ChangeState(enemy1.meleeAttackState);
        }
        else if (performLongRangeAction)
        {            
            stateMashine.ChangeState(enemy1.chageState);
        }
        else if (!isPlayerInMaxAgroRange) 
        {
            stateMashine.ChangeState(enemy1.lookForPlayerState);
        }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
