using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWeaponStyle : WeaponStyle
{
    public override void ActivateFirstUpgrade()
    {
        Debug.Log("Crimson Carnage: Unleashes a devastating finishing move. This finishing move creates a shockwave of crimson energy, dealing massive damage to surrounding enemies.");
    }

    // Переопределение метода для активации второго апгрейда красного стиля оружия
    public override void ActivateSecondUpgrade()
    {
        Debug.Log("Blood Harvest: Permanently increases maximum health. Additionally, successful attacks restore a small percentage of your health.");
    }

    // Переопределение метода для активации третьего апгрейда красного стиля оружия
    public override void ActivateThirdUpgrade()
    {
        Debug.Log("Sanguine Embrace: Successful critical hits now have a chance to apply a bleeding effect to enemies, causing them to lose health over time. Now your 'Crimson Carnage' applying an intensified bleeding effect.");
    }
    public override void DeactivateEffect()
    {
        base.DeactivateEffect();
    }
}
