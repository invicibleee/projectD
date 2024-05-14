using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_RollState : BossRollState
{
    private EnemyBossOne bossOne;

    public B1_RollState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_BossRollState stateData,EnemyBossOne bossOne) : base(stateMashine, enemy, animBoolName, attackPosition, stateData)
    {
        this.bossOne = bossOne;
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
 
        if (isRollTimeOver)
        {
            stateMashine.ChangeState(bossOne.rollOverState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
