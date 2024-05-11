using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueWeaponStyle : WeaponStyle
{
    public GameObject celestialNexusPrefab; // ������ ������� � �������� CelestialNexus
    [SerializeField] private float timeToDestroy;

    private GameObject celestialNexusInstance;
    public override void ActivateFirstUpgrade()
    {
        // ���������, ���������� �� ������
        if (celestialNexusPrefab != null)
        {
            // �������� ������ �� ������ �� PlayerManager
            PlayerManager playerManager = PlayerManager.instance;
            if (playerManager != null && playerManager.player != null && celestialNexusInstance != null)
            {
                // ������� ��������� ������� � �������� celestialNexusPrefab ����� � �������
                Instantiate(celestialNexusPrefab, playerManager.player.transform.position + playerManager.player.transform.right * 2f, Quaternion.identity);

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

    // ��������������� ������ ��� ��������� ������� �������� �������� ����� ������
    public override void ActivateSecondUpgrade()
    {
    }

    // ��������������� ������ ��� ��������� �������� �������� �������� ����� ������
    public override void ActivateThirdUpgrade()
    {

    }
    public override void DeactivateEffect()
    {
        base.DeactivateEffect();
    }
}
