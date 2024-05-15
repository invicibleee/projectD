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
    private HealthFlask healthFlask;

    public void Start()
    {
        flaskGUI = FindObjectOfType<FlaskGUI>();
        healthFlask = FindObjectOfType<HealthFlask>();
    }

    public override void ActivateEffect()
    {
        base.ActivateEffect();
        playerStats.onDamageDealt += ApplyVampirism;
        healthFlask.CanUseFlasks(false);
        flaskGUI.Deactivate();
        Debug.Log("HP ADDED:" + hpAdded);
    }
    public override void DeactivateEffect()
    {
        playerStats.onDamageDealt -= ApplyVampirism;

        playerStats.maxHealth.RemoveModifier(hpAdded);
        healthFlask.CanUseFlasks(true);
        flaskGUI.Activate();

        playerStats.damageReceivedReductionPercentage -= damageReductionPercentage;
        Debug.Log("HP REMOVED:" + hpAdded);
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

