using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRollIsOverState : EnemyState
{
    protected D_BossRollIsOverState stateData;

    protected bool isPlayerInMaxAgroRange;
    protected bool isPlayerInMinAgroRange;
    protected bool performCloseRangeAction;
    protected bool isRollTimeDone; 
    protected float rollDoneTime;
    public BossRollIsOverState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_BossRollIsOverState stateData) : base(stateMashine, enemy, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMaxAgroRange = enemy.CheckPlayerInMaxAgroRange();
        isPlayerInMinAgroRange = enemy.CheckPlayerInMinAgroRange();
        performCloseRangeAction = enemy.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();
        SetRandomIdleTime();
        isRollTimeDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time > startTime + rollDoneTime)
        {
            isRollTimeDone = true;
        }
    }
    public void SetRandomIdleTime()
    {
        rollDoneTime = Random.Range(stateData.minTimetOverRoll, stateData.maxTimeOverRoll);
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
