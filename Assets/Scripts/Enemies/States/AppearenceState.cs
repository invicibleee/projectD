using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearenceState : EnemyState
{
    protected D_AppearenceState stateData;
    protected bool isPlayerInMinArgoRange;
    protected bool isAppearenced;
    public AppearenceState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_AppearenceState stateData) : base(stateMashine, enemy, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinArgoRange = enemy.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        isAppearenced = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time>= startTime + stateData.animSpeed)
        {
            isAppearenced=true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
