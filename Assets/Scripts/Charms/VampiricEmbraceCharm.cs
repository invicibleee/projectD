using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampiricEmbraceCharm : Charm
{
    public float maxHPIncreasePercentage;
    public float vampirismPercentage;
    public float damageReductionPercentage;
    private float hpAdded;

    private FlaskGUI flaskGUI;

    public void Start()
    {
        flaskGUI = FindObjectOfType<FlaskGUI>();
    }

    public override void ActivateEffect()
    {
        base.ActivateEffect();
        playerStats.onDamageDealt += ApplyVampirism;
        flaskGUI.Deactivate();
    }
    public override void DeactivateEffect()
    {
        playerStats.onDamageDealt -= ApplyVampirism;

        hpAdded = playerStats.maxHealth.GetValue() * (maxHPIncreasePercentage / 100f);
        playerStats.maxHealth.RemoveModifier(hpAdded);
        flaskGUI.Activate();
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

