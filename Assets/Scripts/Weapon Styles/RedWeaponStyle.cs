using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWeaponStyle : WeaponStyle
{

    [Header("Base Upgrade")]
    [SerializeField] public CrimsonCarnage crimsonCarnage;
    [Header("First Upgrade")]
    [SerializeField] private float maxHealthIncrease;
    [SerializeField] private float vampirismPercentage;
    [Header("Second Upgrade")]
    [SerializeField] public float bleedingDamage;
    [SerializeField] public float bleedTickPerSecond;
    [SerializeField] public float bleedDuration; public float originalBleedDuration;
    private float damageTimer = 0f;
    public bool isBleeding;

    public bool isBaseActive;
    public bool isFirstActive;
    public bool isSecondActive;



    private PlayerStats playerStats;
    private Player player;
    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        player = PlayerManager.instance.player;
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
        //if (Input.GetKeyUp(KeyCode.F) && isBaseActive && playerStats.currentUlt == playerStats.maxUlt.GetValue())
        //{
        //    bleedDuration = originalBleedDuration;
        //    crimsonCarnage.ActivateCrimsonCarnage();

        //    isBleeding = true;
        //}
        
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
