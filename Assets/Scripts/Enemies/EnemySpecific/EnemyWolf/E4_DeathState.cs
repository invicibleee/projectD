using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_DeathState : DeathState
{
    private EnemyWolf enemyWolf;
    public E4_DeathState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_DeathState stateData, EnemyWolf enemyWolf) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemyWolf = enemyWolf;
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
