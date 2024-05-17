using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E9_DeathState : DeathState
{
    private EnemyAsassin enemyAsassin;
    public E9_DeathState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_DeathState stateData, EnemyAsassin enemyAsassin) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemyAsassin = enemyAsassin;
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
