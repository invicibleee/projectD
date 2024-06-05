using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_BackTeleportState : BackTeleportState
{
    private EnemyBossTwo bosstwo;
    public B2_BackTeleportState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_BackTeleportState stateData, Player player, EnemyBossTwo bosstwo) : base(stateMashine, enemy, animBoolName, stateData, player)
    {
        this.bosstwo = bosstwo;
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
            stateMashine.ChangeState(bosstwo.meleeAttackState);
        }
        else if (!isPLayerInMaxArgroRange)
        {
            stateMashine.ChangeState(bosstwo.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
