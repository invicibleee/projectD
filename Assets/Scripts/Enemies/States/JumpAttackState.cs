using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackState : EnemyState
{
    protected D_JumpAttackState stateData;

    protected bool isGrounded;
    protected bool isJumped;
    protected bool isPlayerInMinArgoRange;
    protected bool isPlayerInMaxArgoRange;
    protected float startTimeB;

    public JumpAttackState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName,D_JumpAttackState stateData) : base(stateMashine, enemy, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMaxArgoRange = enemy.CheckPlayerInMaxAgroRange();
        isPlayerInMinArgoRange = enemy.CheckPlayerInMinAgroRange();
        isGrounded = enemy.IsGroundDetected();
    }

    public override void Enter()
    {
        base.Enter();
        startTimeB = Time.time;
        isJumped = false;
        enemy.SetVelocityEnemy(stateData.jumpSpeed, stateData.jumpAngle, -enemy.facingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }



    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTimeB + stateData.jumpTime)
        {
            isJumped = true;
            Debug.Log(isJumped);
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
