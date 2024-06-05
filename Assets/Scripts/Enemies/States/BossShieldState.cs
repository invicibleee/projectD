using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BossShieldState : EnemyState
{
    private D_BossShieldState stateData;
    protected float shieldTime;
    protected bool isShieldTimeOver;
    protected float armor;
    protected bool performCloseRangePosition;
    public BossShieldState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_BossShieldState stateData) : base(stateMashine, enemy, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        SetRandomShieldTime();
        isShieldTimeOver = false;
        performCloseRangePosition = false;
        enemy.stats.armor.AddModifier(stateData.armotboost);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= startTime + shieldTime)
        {
            isShieldTimeOver = true;
        }
       // Debug.Log(isShieldTimeOver);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.stats.armor.RemoveModifier(stateData.armotboost);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        performCloseRangePosition = enemy.CheckPlayerInCloseRangeAction();
    }
    public void SetRandomShieldTime()
    {
        shieldTime = Random.Range(stateData.minShieldTime, stateData.maxShieldTime);
    }
}
