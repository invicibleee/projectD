using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : Skill
{
    private float originalCooldown; // Оригинальное значение кулдауна

    protected override void Start()
    {
        base.Start();
        originalCooldown = cooldown; // Сохраняем оригинальное значение кулдауна
    }

    public override void UseSkill()
    {
        base.UseSkill();
        Debug.Log("Created clone behind");
    }

    // Метод для изменения кулдауна скилла
    public void ReduceCooldown(float reductionAmount)
    {
        cooldown = originalCooldown - reductionAmount; // Уменьшаем кулдаун
    }
    public void SetDefaultCooldown()
    {
        cooldown = originalCooldown; 
    }
}
