using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AegisAmuletCharm : Charm
{
    public float damageReductionPercentage;
    public float reflectedDamage;
    public override void CharmEffect()
    {
        playerStats.damageReceivedReductionPercentage = damageReductionPercentage;
    }
}
