using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampiricEmbraceCharm : Charm
{
    public float maxHPIncreasePercentage;
    public float vampirismPercentage;
    public float damageReductionPercentage;
    private float hpAdded;

    public override void ActivateEffect()
    {
        base.ActivateEffect();
        playerStats.onDamageDealt += ApplyVampirism;
    }
    public override void DeactivateEffect()
    {
        playerStats.onDamageDealt -= ApplyVampirism;

        hpAdded = playerStats.maxHealth.GetValue() * (maxHPIncreasePercentage / 100f);
        playerStats.maxHealth.RemoveModifier(hpAdded);
    }

    public override void CharmEffect()
    {
        hpAdded = playerStats.maxHealth.GetValue() * (maxHPIncreasePercentage / 100f);
        playerStats.maxHealth.AddModifier(hpAdded);
        playerStats.allBars.UpdateBars();

        playerStats.damageReceivedReductionPercentage += damageReductionPercentage;
    }
    private void ApplyVampirism(float damageDealt)
    {
        float healAmount = damageDealt * (vampirismPercentage / 100f);
        playerStats.IncreaseHealthBy(healAmount);
        playerStats.allBars.SetHealth(playerStats.currentHealth, playerStats.maxHealth.GetValue());
    }
   
}

