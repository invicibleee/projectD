using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E10_DebafState : DebafState
{
    private EnemyDebafGhost debafGhost;
    public E10_DebafState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Player player, D_DebafState stateData, EnemyDebafGhost debafGhost) : base(stateMashine, enemy, animBoolName, player, stateData)
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
        if (!isPlayerInMaxAgroRange)
        {
            if (speedDebafApplied)
            {
                stateMashine.ChangeState(debafGhost.lookForPLayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
