using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsassinationBackTeleportState : BackTeleportState
{
    public AsassinationBackTeleportState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_BackTeleportState stateData, Player player) : base(stateMashine, enemy, animBoolName, stateData, player)
    {

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
    public void OnTriggerEnter()
    {

    }
}
