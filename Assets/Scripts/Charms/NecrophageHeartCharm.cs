using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecrophageHeartCharm : Charm
{
    [SerializeField] private float essenceDropIncreasePercentage = 0.2f; // Например, 20%

    public override void CharmEffect()
    {
        // Подписываемся на событие смерти врага
        EnemyStats.OnEnemyDeath += IncreaseEssenceDrop;
    }
    public override void ActivateEffect()
    {
        base.ActivateEffect();
    }
    public override void DeactivateEffect()
    {
        // Отписываемся от события смерти врага при деактивации чарма
        EnemyStats.OnEnemyDeath -= IncreaseEssenceDrop;
    }

    // Метод, который будет вызываться при смерти врага
    private void IncreaseEssenceDrop(EnemyStats enemyStats)
    {
        // Увеличиваем количество дропающихся ессенций на процент из чарма
        enemyStats.essenceDropAmount = Mathf.RoundToInt(enemyStats.essenceDropAmount + (enemyStats.essenceDropAmount * essenceDropIncreasePercentage));
    }
}
