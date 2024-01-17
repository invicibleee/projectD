using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : EnemyState
{

    protected D_StunState stateData;

    protected bool isGrounded;

    protected bool isStunTimeOver;

    protected bool isMovementStopped;

    protected bool preformCloseRangeAction;

    protected bool isPlayerInMinAgroRange;
    public StunState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_StunState stateData) : base(stateMashine, enemy, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = enemy.CheckGourndAround();
        preformCloseRangeAction= enemy.CheckPlayerInCloseRangeAction();
        isPlayerInMinAgroRange = enemy.CheckPlayerInMinAgroRange();

    }

    public override void Enter()
    {
        base.Enter();
        isStunTimeOver = false;
        isMovementStopped = false;
        enemy.SetVelocityEnemy(stateData.stunKnockbackSpeed, stateData.stunKnockbackAngle, enemy.lastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= startTime + stateData.stunTime)
        {
            isStunTimeOver = true;
        }
        if(isGrounded && Time.time >= startTime + stateData.stunKnockbackTime && !isMovementStopped)
        {
            isMovementStopped = true;
            enemy.SetVelocityEnemy(0f);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
