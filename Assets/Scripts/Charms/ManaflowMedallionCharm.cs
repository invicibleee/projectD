using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaflowMedallionCharm : Charm
{
    [SerializeField] private float maxManaIncrease; // «начение увеличени€ максимальной маны
    [SerializeField] private float manaRegenerationRate; //  оличество восстанавливаемого здоровь€ при попадании

    public override void ActivateEffect()
    {
        base.ActivateEffect();
    }

    public override void DeactivateEffect()
    {
        base.DeactivateEffect();
        playerStats.maxMana.RemoveModifier(maxManaIncrease);
        playerStats.manaRegenRate.RemoveModifier(manaRegenerationRate);
        playerStats.allBars.SetMana(playerStats.currentMana, playerStats.maxMana.GetValue());
    }
    public override void CharmEffect()
    {
        playerStats.maxMana.AddModifier(maxManaIncrease);
        playerStats.manaRegenRate.AddModifier(manaRegenerationRate);
        playerStats.allBars.SetMana(playerStats.currentMana, playerStats.maxMana.GetValue());
    }
}
