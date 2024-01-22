using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : EnemyState
{
    protected D_DodgeState stateData;

    protected bool performCloseRangeAction;
    protected bool isPLayerInMaxArgroRange;
    protected bool isGrounded;
    protected bool isDodgeOver;
    public DodgeState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_DodgeState stateData) : base(stateMashine, enemy, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        performCloseRangeAction = enemy.CheckPlayerInCloseRangeAction();
        isPLayerInMaxArgroRange = enemy.CheckPlayerInMaxAgroRange();
        isGrounded = enemy.IsGroundDetected();
    }

    public override void Enter()
    {
        base.Enter();
        isDodgeOver = false;
        enemy.SetVelocityEnemy(stateData.dodgeSpeed, stateData.dodgeAngle, -enemy.facingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >=  startTime + stateData.dodgeTime && isGrounded)
        {
            isDodgeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
