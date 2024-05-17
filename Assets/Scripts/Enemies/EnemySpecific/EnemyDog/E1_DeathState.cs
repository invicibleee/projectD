using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class E1_DeathState : DeathState
{
    private EnemyDog enemyDog;
    public E1_DeathState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_DeathState stateData, EnemyDog enemyDog) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemyDog = enemyDog;
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
