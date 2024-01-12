using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected FiniteStateMashine stateMashine;
    protected Entity entity;

    protected float startTime;

    protected string animBoolName;


    public State(FiniteStateMashine stateMashine, Entity entity, string animBoolName)
    {
        this.stateMashine = stateMashine;
        this.entity = entity;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);
        DoChecks();
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName, false);

    }

    public virtual void PhysicsUpdate() 
    { 
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }

}
