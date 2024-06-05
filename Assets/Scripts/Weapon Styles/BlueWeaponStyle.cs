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
    [SerializeField] private float magicDamageIncreasePerOrb;
    [SerializeField] private int maxOrbCount = 3;
    [SerializeField] private int currentOrbCount;
    [SerializeField] private float timeToSpawnSphere;
    private bool isAddingSphere = false;

    private bool isBaseActive;
    private bool isFirstActive;
    private bool isSecondActive;

    private PlayerStats playerStats;
    private SphereGUI sphereGUI;
    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        sphereGUI = FindObjectOfType<SphereGUI>();

    }
    private void Update()
    {
        if (Input.GetKeyUp(InputSettings.Instance.GetKeyForAction(InputSettings.Instance.Button_ability)) && isBaseActive && playerStats.currentUlt == playerStats.maxUlt.GetValue())
        {
            ActivateCelestialNexus();
        }
        if (isSecondActive && currentOrbCount < maxOrbCount && !isAddingSphere)
        {
            StartCoroutine(AddSphere());
        }
    }

    private void RemoveAllSpheres(float remove)
    {
        currentOrbCount = 0;
        playerStats.magicAmplify.RemoveAllModifiers();
        for (int i = 0; i < sphereGUI.images.Length; i++)
        {
            sphereGUI.ClearImage(i);
        }
    }

    private IEnumerator AddSphere()
    {
        if (currentOrbCount < maxOrbCount)
        {
            isAddingSphere = true; // ������������� ����, ��� �������� ���������� ����
            yield return new WaitForSeconds(timeToSpawnSphere);
            currentOrbCount++;
            sphereGUI.FillImage(currentOrbCount - 1);
            playerStats.magicAmplify.AddModifier(magicDamageIncreasePerOrb);
            isAddingSphere = false; // ���������� ���� ����� ���������� ����
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

    public override void ActivateThirdUpgrade()
    {
        isSecondActive = true;
        sphereGUI.Activate();
        playerStats.OnDamageReceived += RemoveAllSpheres;
        
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
        if(isSecondActive)
        {
            RemoveAllSpheres(0);
            sphereGUI.Deactivate();
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
