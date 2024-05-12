using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayerState : EnemyState
{
    protected D_LookForPlayer stateData;

    protected bool turnImmediately;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;
    protected bool performCloseRangeAction;

    protected float lastTurnTime;

    protected int amountOfTurnsDone;
    public LookForPlayerState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_LookForPlayer stateData) : base(stateMashine, enemy, animBoolName)
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

        enemy.rb.bodyType = RigidbodyType2D.Dynamic;
        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;
        lastTurnTime = startTime;
        amountOfTurnsDone = 0;
        enemy.SetVelocityEnemy(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(turnImmediately)
        {
            enemy.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            turnImmediately = false;
        }else if (Time.time > lastTurnTime + stateData.timeBetweenTunrs && !isAllTurnsDone)
        {
            enemy.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
        }

        if (amountOfTurnsDone >= stateData.amountOfTurns)
        {
            isAllTurnsDone = true;
        }

        if(Time.time >= lastTurnTime + stateData.timeBetweenTunrs && isAllTurnsDone)
        {
            isAllTurnsTimeDone=true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetTunrImmediatly(bool flip)
    {
        turnImmediately = flip;
    }
}
