using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeTripleTargetAttackState : AttackState
{
    protected D_RangeTripleTargetAttackState stateData;
    protected ProjectileTarget projectileTargetScript;

    protected GameObject projectile;
    protected Transform secondAttackPosition;
    protected Transform thirdAttackPosition;
    protected bool isPlayerInMaxAgroRange;

    protected int numberOfShots; // Лічильник вистрілів

    public int countOfShots;
    public RangeTripleTargetAttackState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Transform attackPosition, D_RangeTripleTargetAttackState stateData,Transform secondAttackPosition, Transform thirdAttackPosition) : base(stateMashine, enemy, animBoolName, attackPosition)
    {
        this.stateData = stateData;
        this.secondAttackPosition = secondAttackPosition;
        this.thirdAttackPosition = thirdAttackPosition;

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
        SetRandomShotTimes();
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
        // Створення проектілів у трьох різних точках
        GameObject projectile1 = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
        GameObject projectile2 = GameObject.Instantiate(stateData.projectile, secondAttackPosition.position, secondAttackPosition.rotation);
        GameObject projectile3 = GameObject.Instantiate(stateData.projectile, thirdAttackPosition.position, thirdAttackPosition.rotation);

        // Налаштування параметрів для кожного проектілу
        ProjectileTarget projectileTargetScript1 = projectile1.GetComponent<ProjectileTarget>();
        projectileTargetScript1.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage, stateData.pursuitTime, stateData.timeToDestroy);

        ProjectileTarget projectileTargetScript2 = projectile2.GetComponent<ProjectileTarget>();
        projectileTargetScript2.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage, stateData.pursuitTime, stateData.timeToDestroy);

        ProjectileTarget projectileTargetScript3 = projectile3.GetComponent<ProjectileTarget>();
        projectileTargetScript3.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage, stateData.pursuitTime, stateData.timeToDestroy);
        numberOfShots++; // Збільшення лічильника вистрілів
    }
    public void SetRandomShotTimes()
    {
        countOfShots = Random.Range(stateData.minCountOfShots, stateData.maxCountOfShots);
    }
}
