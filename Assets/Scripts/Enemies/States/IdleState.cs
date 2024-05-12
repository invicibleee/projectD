using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    protected D_IdleState stateData;

    protected bool flipAfterIdle;

    protected float idleTime;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performCloseRangeAction;

    public IdleState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_IdleState stateData) : base(stateMashine, enemy, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = enemy.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = enemy.CheckPlayerInMaxAgroRange();
        performCloseRangeAction = enemy.CheckPlayerInCloseRangeAction();

    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetVelocityEnemy(0f);
        isIdleTimeOver = false;
        SetRandomIdleTime();
    }
    public override void Exit() 
    { 
        base.Exit();

        if (flipAfterIdle)
        {
            enemy.Flip();
        }
    
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time > startTime + idleTime) 
        { 
            isIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;

    }

    public void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime,stateData.maxIdleTime);
    }
}


