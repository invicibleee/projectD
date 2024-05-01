using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E7_BackTeleportState : BackTeleportState
{
    private EnemyGhost enemyGhost;
    public E7_BackTeleportState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_BackTeleportState stateData, EnemyGhost enemyGhost, Player player) : base(stateMashine, enemy, animBoolName, stateData, player)
    {
        this.enemyGhost = enemyGhost;
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
        if (isPLayerInMaxArgroRange && performCloseRangeAction)
        {
            stateMashine.ChangeState(enemyGhost.meleeAttackState);
        }
        else if (!isPLayerInMaxArgroRange)
        {
            stateMashine.ChangeState(enemyGhost.lookForPlayerState);
        }
      
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
