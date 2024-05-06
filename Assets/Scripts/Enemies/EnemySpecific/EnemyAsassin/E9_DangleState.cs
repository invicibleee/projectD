using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E9_DangleState : DangleState
{
    private EnemyAsassin enemyAsassin;
    public E9_DangleState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, EnemyAsassin enemyAsassin) : base(stateMashine, enemy, animBoolName)
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
        if (isPLayerUnder)
        {
            stateMashine.ChangeState(enemyAsassin.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
