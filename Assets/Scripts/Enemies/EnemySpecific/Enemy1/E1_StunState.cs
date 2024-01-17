using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_StunState : StunState
{
    private EnemyDog enemy1;
    public E1_StunState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_StunState stateData, EnemyDog enemy1) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemy1 = enemy1;
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
        if(isStunTimeOver)
        {
            if(preformCloseRangeAction)
            {
                stateMashine.ChangeState(enemy1.meleeAttackState);
            }
            else if (isPlayerInMinAgroRange)
            {
                stateMashine.ChangeState(enemy1.chageState);
            }
            else
            {
                enemy1.lookForPlayerState.SetTunrImmediatly(true);
                stateMashine.ChangeState(enemy1.lookForPlayerState);

            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
