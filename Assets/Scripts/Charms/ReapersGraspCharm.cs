using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReapersGraspCharm : Charm
{
    public float meleeAttackRangeIncreasePercentage;

    private float originalMeleeAttackRange;

    public override void ActivateEffect()
    {
        // Сохраняем исходное значение дальности атаки перед применением эффекта
        originalMeleeAttackRange = playerStats.GetComponent<Entity>().GetAttackCheckRadius();

        // Применяем эффект чарма
        base.ActivateEffect();
    }

    public override void DeactivateEffect()
    {
        // Возвращаем значение дальности атаки к исходному
        playerStats.GetComponent<Entity>().SetAttackCheckRadius(originalMeleeAttackRange);

        // Вызываем метод базового класса для деактивации чарма
        base.DeactivateEffect();
    }

    public override void CharmEffect()
{
    float currentRadius = playerStats.GetComponent<Entity>().GetAttackCheckRadius();
    float newRadius = currentRadius * (1 + meleeAttackRangeIncreasePercentage / 100f);
    playerStats.GetComponent<Entity>().SetAttackCheckRadius(newRadius);
}

}
