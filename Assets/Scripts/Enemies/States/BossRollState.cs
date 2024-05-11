using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRollState : AttackState
{
    protected D_BossRollState stateData;
    protected AttackDetails attackDetails;
    protected float rollTime;

    protected bool flipAfterRoll;
    protected bool isRollTimeOver;
    protected bool isGrounded;
    protected bool isWallDetected;
    protected bool isPlayerInMaxAgroRange;

    public BossRollState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition,D_BossRollState stateData) : base(stateMashine, enemy, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = enemy.IsGroundDetected();
        isWallDetected = enemy.IsWallDetected();
        isPlayerInMaxAgroRange = enemy.CheckPlayerInMaxAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocityEnemy(stateData.rollSpeed, stateData.rollAngle, enemy.facingDirection);
        isRollTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.angleTime && isGrounded)
        {
            enemy.SetVelocityEnemy(stateData.rollSpeed);
        }
        if (Time.time > startTime + rollTime)
        {
            isRollTimeOver = true;
        }
        if(isWallDetected)
        {
            enemy.Flip();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    public void SetRandomIdleTime()
    {
        rollTime = Random.Range(stateData.minRollTime, stateData.maxRollTime);
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("Damage", attackDetails);
        }
    }
}
