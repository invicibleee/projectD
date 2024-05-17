using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_DeathState : DeathState
{
    private EnemyBossOne _enemyBossOne;
    public B1_DeathState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_DeathState stateData, EnemyBossOne enemyBossOne) : base(stateMashine, enemy, animBoolName, stateData)
    {
        _enemyBossOne = enemyBossOne;
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
