using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_DeathState : DeathState
{
    private EnemyArcher enemyArcher;
    public E2_DeathState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_DeathState stateData, EnemyArcher enemyArcher) : base(stateMashine, enemy, animBoolName, stateData)
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

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
