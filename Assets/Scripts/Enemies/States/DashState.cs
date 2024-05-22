using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : AttackState
{
    protected D_DashState stateData;
    protected AttackDetails attackDetails;
    protected bool isGrounded;

    public DashState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_DashState stateData) : base(stateMashine, enemy, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = enemy.IsGroundDetected();
    }

    public override void Enter()
    {
        base.Enter();
        attackDetails.damageAmount = stateData.attackDamage;
        attackDetails.position = enemy.rb.transform.position;

        // Визначаємо напрямок Dash (може змінюватися залежно від умов)
        Vector2 dashDirection = CalculateDashDirection();
        // Виконуємо рух
        enemy.rb.velocity = dashDirection * (stateData.dashDistance / stateData.dashTime) * stateData.dashSpeed;
    }

    private Vector2 CalculateDashDirection()
    {
        Vector2 inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (inputDirection.magnitude > 0.1f) // перевірка, чи користувач вводить напрямок
            return inputDirection.normalized;
        else // якщо користувач не вводить напрямок, беремо напрямок взагалі
            return enemy.transform.right; // наприклад, вправо
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
        if (Time.time >= startTime + stateData.dashTime && isGrounded)
        {
            enemy.rb.velocity = Vector2.zero;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            PlayerStats target = collider.GetComponent<PlayerStats>();
            enemy.stats.DoDamage(target);
            //collider.transform.SendMessage("Damage", attackDetails);
        }
    }
}
