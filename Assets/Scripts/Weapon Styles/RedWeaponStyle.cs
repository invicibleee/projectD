using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWeaponStyle : WeaponStyle
{

    [Header("Base Upgrade")]
    [SerializeField] private CrimsonCarnage crimsonCarnage;
    [Header("First Upgrade")]
    [SerializeField] private float maxHealthIncrease;
    [SerializeField] private float vampirismPercentage;
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
            crimsonCarnage.ActivateCrimsonCarnage();
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
        playerStats.maxHealth.AddModifier(maxHealthIncrease);
        playerStats.onDamageDealt += ApplyVampirism;
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
            playerStats.maxHealth.RemoveModifier(maxHealthIncrease);
            playerStats.onDamageDealt -= ApplyVampirism;
        }


        isBaseActive = false;
        isFirstActive = false;
        isSecondActive = false;

    }
    private void ApplyVampirism(float damageDealt)
    {
        float healAmount = damageDealt * (vampirismPercentage / 100f);
        playerStats.IncreaseHealthBy(healAmount);
        playerStats.allBars.SetHealth(playerStats.currentHealth, playerStats.maxHealth.GetValue());
    }
}
