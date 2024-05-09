using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStyle : MonoBehaviour
{
    public int styleIndex;
    public virtual void ActivateFirstUpgrade()
    {
    }

    // Активирует второй апгрейд стиля оружия
    public virtual void ActivateSecondUpgrade()
    {

    }

    // Активирует третий апгрейд стиля оружия
    public virtual void ActivateThirdUpgrade()
    {

    }

    // Отключает эффекты стиля оружия
    public virtual void DeactivateEffect()
    {
    }
}
