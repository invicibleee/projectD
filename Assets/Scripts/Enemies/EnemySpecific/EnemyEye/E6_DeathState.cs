using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E6_DeathState : DeathState
{
    private EnemyEye enemyEye;
    public E6_DeathState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_DeathState stateData, EnemyEye enemyEye) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemyEye = enemyEye;
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
