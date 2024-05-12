using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GreenWeaponStyle : WeaponStyle
{
    [Header("Base Upgrade")]
    [Header("First Upgrade")]
    [SerializeField] private float moveSpeedIncreasePercent;
    [SerializeField] private float reduceCooldown;

    private bool isBaseActive;
    private bool isFirstActive;
    private bool isSecondActive;

    [SerializeField] private MiracleMirage miracleMirage; 
    private Player player;
    private PlayerStats playerStats;
    private float originalSpeed;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerStats = FindObjectOfType<PlayerStats>();
        originalSpeed = player.moveSpeed;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && isBaseActive && playerStats.currentUlt == playerStats.maxUlt.GetValue())
        {
            miracleMirage.ActivateMirage();
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
        player.moveSpeed += player.moveSpeed * moveSpeedIncreasePercent/100;
        SkillManager.instance.dash.ReduceCooldown(reduceCooldown);
    }

    // Переопределение метода для активации третьего апгрейда красного стиля оружия
    public override void ActivateThirdUpgrade()
    {
        isSecondActive = true;
        SkillManager.instance.clone.CanCreateCloneOnStart(true);
    }
    public override void DeactivateEffect()
    {
        base.DeactivateEffect();
        if(isFirstActive)
        {
            player.moveSpeed = originalSpeed;
            SkillManager.instance.dash.SetDefaultCooldown();
        }
        if (isSecondActive)
        {
            SkillManager.instance.clone.CanCreateCloneOnStart(false);
        }

        isBaseActive = false;
        isFirstActive = false;
        isSecondActive = false;
    }
}
