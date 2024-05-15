using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    public static event Action<EnemyStats> OnEnemyDeath;

    public int essenceDropAmount;
    private PlayerStats playerStats;
    protected override void Start()
    {
        base.Start();

        enemy = GetComponent<Enemy>();
        playerStats = FindObjectOfType<PlayerStats>();
    }

    public override void TakeDamage(float _damage)
    {
        base.TakeDamage(_damage);
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();
        OnEnemyDeath?.Invoke(this);
        PlayerManager.instance.essenceAmount += essenceDropAmount;

        
    }
}
