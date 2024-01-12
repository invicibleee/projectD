using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy;
    public E1_PlayerDetectedState(FiniteStateMashine stateMashine, Entity entity, string animBoolName, D_PlayerDetected stateData, Enemy1 enemy) : base(stateMashine, entity, animBoolName, stateData)
    {
        this.enemy = enemy;
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
            stateMashine.ChangeState(enemy.meleeAttackState);
        }
        else if (performLongRangeAction)
        {            
            stateMashine.ChangeState(enemy.chageState);
        }
        else if (!isPlayerInMaxAgroRange) 
        {
            stateMashine.ChangeState(enemy.lookForPlayerState);
        }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
