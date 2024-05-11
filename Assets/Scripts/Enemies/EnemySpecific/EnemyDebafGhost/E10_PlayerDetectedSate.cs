using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E10_PlayerDetectedSate : PlayerDetectedState
{
    private EnemyDebafGhost debafGhost;

    public E10_PlayerDetectedSate(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_PlayerDetected stateData, EnemyDebafGhost debafGhost) : base(stateMashine, enemy, animBoolName, stateData)
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
        if (performLongRangeAction)
        {
            stateMashine.ChangeState(debafGhost.debafState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMashine.ChangeState(debafGhost.lookForPLayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
