using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E5_DeathState : DeathState
{
    private EnemyWoodGoblin woodGoblin;
    public E5_DeathState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_DeathState stateData, EnemyWoodGoblin woodGoblin) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.woodGoblin = woodGoblin;
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
