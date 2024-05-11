using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E10_IdleState : IdleState
{
    private EnemyDebafGhost enemyDebaf;
    public E10_IdleState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_IdleState stateData, EnemyDebafGhost enemyDebaf) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemyDebaf = enemyDebaf;
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
        if (isPlayerInMinAgroRange)
        {
            stateMashine.ChangeState(enemyDebaf.playerDetectedSate);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
