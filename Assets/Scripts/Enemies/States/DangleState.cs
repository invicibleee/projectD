using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangleState : EnemyState
{
    protected bool isPLayerUnder;
    protected bool isGrounded;
    protected bool isHited;
    public DangleState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName) : base(stateMashine, enemy, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPLayerUnder = enemy.IsPlayerUnderDetected();
        isGrounded = enemy.IsGroundDetected();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
