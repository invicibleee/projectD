using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_DeathState : DeathState
{
    private EnemyBossTwo bossTwo;
    public B2_DeathState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_DeathState stateData, EnemyBossTwo bossTwo) : base(stateMashine, enemy, animBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
