using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueWeaponStyle : WeaponStyle
{
    public GameObject celestialNexusPrefab; // ������ ������� � �������� CelestialNexus
    private GameObject celestialNexusInstance;
    [SerializeField] private float timeToDestroy;
    public override void ActivateFirstUpgrade()
    {
        // ���������, ���������� �� ������
        if (celestialNexusPrefab != null)
        {
            // �������� ������ �� ������ �� PlayerManager
            PlayerManager playerManager = PlayerManager.instance;
            if (playerManager != null && playerManager.player != null)
            {
                // ������� ��������� ������� � �������� celestialNexusPrefab ����� � �������
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
