using UnityEngine;

public class GlassCannonCharm : Charm
{
    public float damageIncreasePercentage = 25f;
    public float damageReceivedIncreasePercentage = 25f;
    private float newDamage;
    private float originalDamage;
    private float originalDamageReceivedReductionPercentage;
    public override void ActivateEffect()
    {
        originalDamage = playerStats.damage.GetValue();
        originalDamageReceivedReductionPercentage = playerStats.damageReceivedReductionPercentage;


        base.ActivateEffect();
    }

    public override void DeactivateEffect()
    {
        playerStats.damage.SetDefaultValue(originalDamage);
        playerStats.damageReceivedReductionPercentage = originalDamageReceivedReductionPercentage;
        playerStats.damage.RemoveModifier(newDamage);

        base.DeactivateEffect();
    }
    public override void CharmEffect()
    {
        float currentDamage = playerStats.damage.GetValue();//10
        newDamage = (currentDamage * (1 + damageIncreasePercentage / 100f)) - currentDamage;
        playerStats.damage.AddModifier((float)newDamage);   

        playerStats.damageReceivedReductionPercentage -= damageReceivedIncreasePercentage;
    }
}