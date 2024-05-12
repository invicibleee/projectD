using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueWeaponStyle : WeaponStyle
{
    [Header("Base Upgrade")]
    public GameObject celestialNexusPrefab; // ������ ������� � �������� CelestialNexus
    private GameObject celestialNexusInstance;
    [SerializeField] private float timeToDestroy;
    [Header("First Upgrade")]
    [SerializeField] private float maxManaIncrease; // �������� ���������� ������������ ����
    [SerializeField] private float manaRegenerationRate; // ���������� ������������������ �������� ��� ���������
    [Header("Second Upgrade")]

    private bool isBaseActive;
    private bool isFirstActive;
    private bool isSecondActive;

    private PlayerStats playerStats;
    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && isBaseActive && playerStats.currentUlt == playerStats.maxUlt.GetValue())
        {
            ActivateCelestialNexus();
        }
    }
    public override void ActivateFirstUpgrade()
    {
        isBaseActive = true;

    }

    // ��������������� ������ ��� ��������� ������� �������� �������� ����� ������
    public override void ActivateSecondUpgrade()
    {
        isFirstActive = true;
        playerStats.maxMana.AddModifier(maxManaIncrease);
        playerStats.manaRegenRate.AddModifier(manaRegenerationRate);
        playerStats.allBars.SetMana(playerStats.currentMana, playerStats.maxMana.GetValue());
    }

    // ��������������� ������ ��� ��������� �������� �������� �������� ����� ������
    public override void ActivateThirdUpgrade()
    {
        isSecondActive = true;
    }
    public override void DeactivateEffect()
    {
        base.DeactivateEffect();
        if (isFirstActive)
        {
            playerStats.maxMana.RemoveModifier(maxManaIncrease);
            playerStats.manaRegenRate.RemoveModifier(manaRegenerationRate);
            playerStats.allBars.SetMana(playerStats.currentMana, playerStats.maxMana.GetValue());
        }

        isBaseActive = false;
        isFirstActive = false;
        isSecondActive = false;
    }

    public void ActivateCelestialNexus()
    {
        if (celestialNexusPrefab != null)
        {
            playerStats.DecreaseUlt(playerStats.maxUlt.GetValue());
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
}
