using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MulticastCharm : Charm
{
    [SerializeField] private float criticalChanceIncrease;
    [SerializeField] private float criticalPowerIncrease;
    [SerializeField] private float fortuneSpeedBonus;
    [SerializeField] private float fortuneDamageBonus;
    [SerializeField] private float fortuneDuration;
    [SerializeField] private float fateShieldBonus;
    [SerializeField] private float fateShieldDuration;

    private bool isFortuneActive = false;
    private bool isFateShieldActive = false;

    private Player player;
    private float originalSpeed;

    private void Start()
    {
        player = PlayerManager.instance.player;
        originalSpeed = player.moveSpeed;
    }
    public override void ActivateEffect()
    {
        base.ActivateEffect();
        playerStats.critChance.AddModifier(criticalChanceIncrease);
        playerStats.critPower.AddModifier(criticalPowerIncrease);
        
    }

    public override void DeactivateEffect()
    {
        base.DeactivateEffect();
        playerStats.critChance.RemoveModifier(criticalChanceIncrease);
        playerStats.critPower.RemoveModifier(criticalPowerIncrease);
        EnemyStats.OnEnemyTakeDamage -= CheckCritical;
        DisableFortune();
        DisableFateShield();
    }
    public override void CharmEffect()
    {
        EnemyStats.OnEnemyTakeDamage += CheckCritical;
    }
    private void CheckCritical(EnemyStats enemyStats)
    {
        if (Random.value * 100 < criticalChanceIncrease)
        {
            if (!isFortuneActive && Random.value <= 0.5)
            {
                isFortuneActive = true;
                player.moveSpeed += fortuneSpeedBonus;
                playerStats.damage.AddModifier(fortuneDamageBonus);
                Invoke("DisableFortune", fortuneDuration);
            }
            if (!isFateShieldActive && Random.value > 0.5)
            {
                isFateShieldActive = true;
                playerStats.armor.AddModifier(fateShieldBonus);
                Invoke("DisableFateShield", fateShieldDuration);
            }
        }
    }
    private void DisableFortune()
    {
        isFortuneActive = false;
        player.moveSpeed = originalSpeed;
        playerStats.damage.RemoveModifier(fortuneDamageBonus);
    }

    private void DisableFateShield()
    {
        isFateShieldActive = false;
        playerStats.armor.RemoveModifier(fateShieldBonus);
    }

}
