using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected D_IdleState stateData;

    protected bool flipAfterIdle;

    protected float idleTime;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinAgroRange;

    public IdleState(FiniteStateMashine stateMashine, Entity entity, string animBoolName, D_IdleState stateData) : base(stateMashine, entity, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelosity(0f);
        isIdleTimeOver = false;
        SetRandomIdleTime();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }
    public override void Exit() 
    { 
        base.Exit();

        if (flipAfterIdle)
        {
            entity.Flip();
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
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();

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


