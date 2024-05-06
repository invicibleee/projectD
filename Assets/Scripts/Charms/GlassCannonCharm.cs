using UnityEngine;

public class GlassCannonCharm : Charm
{
    // Увеличение урона и получаемого урона
    public float damageIncreasePercentage = 25f;
    public float damageReceivedIncreasePercentage = 25f;
    private float newDamage;
    private float originalDamage;
    private float originalDamageReceivedReductionPercentage;
    public override void ActivateEffect()
    {
        // Сохраняем исходные значения перед применением эффекта
        originalDamage = playerStats.damage.GetValue();
        originalDamageReceivedReductionPercentage = playerStats.damageReceivedReductionPercentage;

        // Применяем эффект чарма
        base.ActivateEffect();
    }

    public override void DeactivateEffect()
    {
        // Возвращаем значения к исходным
        playerStats.damage.SetDefaultValue(originalDamage);
        playerStats.damageReceivedReductionPercentage = originalDamageReceivedReductionPercentage;
        playerStats.damage.RemoveModifier(newDamage);
        // Вызываем метод базового класса для деактивации чарма
        base.DeactivateEffect();
    }
    public override void CharmEffect()
    {
        // Увеличение урона цели на определенный процент
        float currentDamage = playerStats.damage.GetValue();//10
        newDamage = (currentDamage * (1 + damageIncreasePercentage / 100f)) - currentDamage;
        playerStats.damage.AddModifier((float)newDamage);   


        // Увеличение получаемого урона целью на определенный процент
        playerStats.damageReceivedReductionPercentage -= damageReceivedIncreasePercentage;
    }
}