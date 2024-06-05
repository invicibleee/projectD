using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class B2_ShieldState : BossShieldState
{
    private EnemyBossTwo bossTwo;
    public B2_ShieldState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_BossShieldState stateData, EnemyBossTwo bossTwo) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.bossTwo = bossTwo;
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
        if (isShieldTimeOver)
        {
            if (performCloseRangePosition)
            {
                stateMashine.ChangeState(bossTwo.meleeAttackState);
            }
            else
            {
                stateMashine.ChangeState(bossTwo.lookForPlayerState);
            }
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
