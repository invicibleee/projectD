using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BerserkersRageCharm : Charm
{
    [SerializeField] private float damageIncreasePercentage;

    private float originalDamage;
    private float damageAdded;
    private bool effectApplied = false;
    private bool isActive = false;

    private void Update()
    {
        // Проверяем, активен ли в данный момент чарм
        if (isActive)
        {
            // Проверка условий только если чарм активен
            if (playerStats.currentHealth < playerStats.maxHealth.GetValue() && !effectApplied)
            {
                // Применяем эффект чарма
                CharmEffect();
            }
            else if (playerStats.currentHealth >= playerStats.maxHealth.GetValue() && effectApplied)
            {
                // Удаляем модификатор урона, если здоровье восстановлено до максимального и эффект был применен
                playerStats.damage.RemoveModifier(damageAdded);
                effectApplied = false;
            }
        }
    }

    public override void ActivateEffect()
    {
        originalDamage = playerStats.damage.GetValue();
        CharmEffect();
        // Сохраняем исходное значение урона перед применением эффекта
        
        isActive = true;
    }

    public override void DeactivateEffect()
    {
        if (effectApplied)
        {
            playerStats.damage.RemoveModifier(damageAdded);
            effectApplied = false;
        }

        // Устанавливаем флаг активности чарма
        isActive = false;
    }

    public override void CharmEffect()
    {
        damageAdded = originalDamage * (damageIncreasePercentage / 100f);
        // Добавляем модификатор урона
        playerStats.damage.AddModifier(damageAdded);
        // Устанавливаем флаг, что эффект применен
        effectApplied = true;
    }
}
