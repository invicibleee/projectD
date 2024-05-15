using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowstepAmuletCharm : Charm
{
    [SerializeField] private float evasionChance;
    public override void CharmEffect()
    {
        playerStats.evasion.AddModifier(evasionChance);
    }
    public override void DeactivateEffect()
    {
        playerStats.evasion.RemoveModifier(evasionChance);
    }
}
