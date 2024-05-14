using UnityEngine;

public class RangeTargetAttackState : AttackState
{
    private D_RangeTargetAttackState stateData;
    protected ProjectileTarget projectileTargetScript;

    protected GameObject projectile;
    protected int numberOfShots; // Лічильник вистрілів

    protected bool isPlayerInMaxAgroRange;
    public RangeTargetAttackState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_RangeTargetAttackState stateData) : base(stateMashine, enemy, animBoolName, attackPosition)
    {
        this.stateData = stateData;
        numberOfShots = 0; // Ініціалізація лічильника вистрілів
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
        projectileTargetScript = projectile.GetComponent<ProjectileTarget>();
        projectileTargetScript.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage, stateData.pursuitTime, stateData.timeToDestroy);
        numberOfShots++; // Збільшення лічильника вистрілів
        Debug.Log(numberOfShots);

    }
}
