using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueWeaponStyle : WeaponStyle
{
    public GameObject celestialNexusPrefab; // Префаб объекта с скриптом CelestialNexus

    public override void ActivateFirstUpgrade()
    {
        // Проверяем, существует ли префаб
        if (celestialNexusPrefab != null)
        {
            // Создаем экземпляр объекта с префабом celestialNexusPrefab
            GameObject celestialNexusObject = Instantiate(celestialNexusPrefab, transform.position + transform.right * 2f, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Celestial Nexus prefab is not assigned!");
        }
    }

    // Переопределение метода для активации второго апгрейда красного стиля оружия
    public override void ActivateSecondUpgrade()
    {
    }

    // Переопределение метода для активации третьего апгрейда красного стиля оружия
    public override void ActivateThirdUpgrade()
    {

    }
    public override void DeactivateEffect()
    {
        base.DeactivateEffect();
    }
}
