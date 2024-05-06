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
}
