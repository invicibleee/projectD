using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_JumpAttackState : JumpAttackState
{
    private EnemyBossOne bossOne;
    public B1_JumpAttackState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName,D_JumpAttackState stateData, EnemyBossOne bossOne) : base(stateMashine, enemy, animBoolName, stateData)
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
        if (isJumped)
        {
           stateMashine.ChangeState(bossOne.jumpAttackDone);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
