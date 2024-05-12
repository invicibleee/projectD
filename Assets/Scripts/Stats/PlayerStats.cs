using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;
    public HealthManaUltBars allBars;

    protected override void Start()
    {
        base.Start();

        player= GetComponent<Player>();
    }

    public override void TakeDamage(float _damage)
    {
        base.TakeDamage(_damage);
        allBars.SetHealth(currentHealth - _damage, GetMaxHealthValue());

    }

    protected override void Die()
    {
        base.Die();
        player.Die();

    }
    public override void DoDamage(CharacterStats _targetStats)
    {
        base.DoDamage(_targetStats);
        IncreaseMana(5);
        IncreaseUlt(10);
        allBars.SetMana(currentMana, maxMana.GetValue());
        allBars.SetUlt(currentUlt, maxUlt.GetValue());
    }
    public override void RegenerateMana()
    {
        base.RegenerateMana();
        allBars.SetMana(currentMana, maxMana.GetValue());
    }
    public override void IncreaseHealthBy(float healAmount)
    {
        base.IncreaseHealthBy(healAmount);
        allBars.SetHealth(currentHealth, GetMaxHealthValue());
    }
    public override void DecreaseUlt(float amount)
    {
        base.DecreaseUlt(amount);
        allBars.SetUlt(currentUlt, GetMaxUltValue());
    }
}
