using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : EnemyState
{
    protected D_ChargeState stateData;

    protected bool isPlayerInMinAgroRange;

    protected bool isDetectingLedge;

    protected bool isDetectingWall;

    protected bool isChargeTimeOver;

    protected bool performCloseRangeAction;
    public ChargeState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_ChargeState stateData) : base(stateMashine, enemy, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = enemy.CheckPlayerInMinAgroRange();
        isDetectingLedge = enemy.IsGroundDetected();
        isDetectingWall = enemy.IsWallDetected();

        performCloseRangeAction = enemy.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();
        isChargeTimeOver = false;
        enemy.SetVelocityEnemy(stateData.chargeSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
