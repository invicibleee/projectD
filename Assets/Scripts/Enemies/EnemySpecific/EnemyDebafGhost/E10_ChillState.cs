using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E10_ChillState : LookForPlayerState
{
    private EnemyDebafGhost debafGhost;
    public E10_ChillState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_LookForPlayer stateData, EnemyDebafGhost debafGhost) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.debafGhost = debafGhost;
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
        if (isPlayerInMaxAgroRange)
        {
            stateMashine.ChangeState(debafGhost.appearence);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
