using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{

    protected D_StunState stateData;
    public StunState(FiniteStateMashine stateMashine, Enemy enemy, string animBoolName, D_StunState stateData) : base(stateMashine, enemy, animBoolName)
    {
        this.stateData = stateData;
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
