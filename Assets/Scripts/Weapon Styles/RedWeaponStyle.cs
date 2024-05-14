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
    [SerializeField] private float bleedingDamage;
    [SerializeField] private float bleedTickPerSecond;
    [SerializeField] private float bleedDuration;private float originalBleedDuration;
    private float damageTimer = 0f;
    private bool isBleeding;

    private bool isBaseActive;
    private bool isFirstActive;
    private bool isSecondActive;



    private PlayerStats playerStats;
    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        originalBleedDuration = bleedDuration;
    }
    private void Update()
    {
        if (isSecondActive && bleedDuration > 0 && isBleeding)
        {
            bleedDuration -= Time.deltaTime;
            damageTimer += Time.deltaTime;
            if (damageTimer >= 1f / bleedTickPerSecond) // Проверяем, прошло ли достаточно времени для тика урона
            {
                damageTimer = 0f; // Сбрасываем таймер
                crimsonCarnage.BleedEffect(bleedingDamage);
            }
            if(bleedDuration == 0)
            {
                isBleeding = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.F) && isBaseActive && playerStats.currentUlt == playerStats.maxUlt.GetValue())
        {
            bleedDuration = originalBleedDuration;
            crimsonCarnage.ActivateCrimsonCarnage();
            isBleeding = true;
        }
        
    }
    public override void ActivateFirstUpgrade()
    {
        isBaseActive = true;
    }

    // Переопределение метода для активации второго апгрейда красного стиля оружия
    public override void ActivateSecondUpgrade()
    {
        isFirstActive = true;
        playerStats.maxHealth.AddModifier(maxHealthIncrease);
        playerStats.onDamageDealt += ApplyVampirism;
    }

    // Переопределение метода для активации третьего апгрейда красного стиля оружия
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
