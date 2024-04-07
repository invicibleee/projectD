using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerAttackState : AttackState
{
    private D_LazerAttackState stateData;

    protected GameObject projectile;
    protected ProjectileLazer projectileLazerScript;
    protected bool isPlayerInMaxAgroRange;
    public LazerAttackState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_LazerAttackState stateData) : base(stateMashine, enemy, animBoolName, attackPosition)
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
        projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
        projectileLazerScript = projectile.GetComponent<ProjectileLazer>();
        projectileLazerScript.FireProjectile(stateData.projectileSpeed,  stateData.projectileDamage);
    }
}
