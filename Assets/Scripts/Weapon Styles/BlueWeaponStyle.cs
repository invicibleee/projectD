using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueWeaponStyle : WeaponStyle
{
    public GameObject celestialNexusPrefab; // Префаб объекта с скриптом CelestialNexus
    private GameObject celestialNexusInstance;
    [SerializeField] private float timeToDestroy;
    public override void ActivateFirstUpgrade()
    {
        // Проверяем, существует ли префаб
        if (celestialNexusPrefab != null)
        {
            // Получаем ссылку на игрока из PlayerManager
            PlayerManager playerManager = PlayerManager.instance;
            if (playerManager != null && playerManager.player != null)
            {
                // Создаем экземпляр объекта с префабом celestialNexusPrefab рядом с игроком
                celestialNexusInstance = Instantiate(celestialNexusPrefab, playerManager.player.transform.position + playerManager.player.transform.right * 2f, Quaternion.identity);
                Destroy(celestialNexusInstance, timeToDestroy);
            }
            else
            {
                Debug.LogError("PlayerManager or player reference is missing!");
            }
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
