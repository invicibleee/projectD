using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected EnemyStateMashine stateMashine;
    protected Enemy enemy;

    protected float startTime;

    protected string animBoolName;


    public EnemyState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName)
    {
        this.stateMashine = stateMashine;
        this.enemy = enemy;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        enemy.anim.SetBool(animBoolName, true);
        DoChecks();
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void Exit()
    {
        enemy.anim.SetBool(animBoolName, false);

    }

    public virtual void PhysicsUpdate() 
    { 
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }

}
