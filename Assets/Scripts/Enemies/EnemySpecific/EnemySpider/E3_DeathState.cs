using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_DeathState : DeathState
{
    private EnemySpider enemySpider;
    public E3_DeathState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_DeathState stateData, EnemySpider enemySpider) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemySpider = enemySpider;
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
