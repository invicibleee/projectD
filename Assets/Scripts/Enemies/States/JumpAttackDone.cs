using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackDone : AttackState
{
    protected D_JumpAttackDone stateData;
    protected AttackDetails attackDetails;
    protected bool isPlayerInMaxAgroRange;
    public JumpAttackDone(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_JumpAttackDone stateData) : base(stateMashine, enemy, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMaxAgroRange = enemy.CheckPlayerInMaxAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetVelocityEnemy(0f);
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
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            PlayerStats target = collider.GetComponent<PlayerStats>();
            enemy.stats.DoDamage(target);
            //collider.transform.SendMessage("Damage", attackDetails);
        }

    }
}
