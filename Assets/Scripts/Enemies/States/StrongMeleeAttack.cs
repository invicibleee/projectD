using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongMeleeAttack : AttackState
{
    protected D_StrongAttackState stateData;

    protected AttackDetails attackDetails;

    protected bool isStrongAttackOver;
    public StrongMeleeAttack(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_StrongAttackState stateData) : base(stateMashine, enemy, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        attackDetails.damageAmount = stateData.attackDamage;
        attackDetails.position = enemy.rb.transform.position;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
