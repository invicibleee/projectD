using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrimsonCarnage : MonoBehaviour
{
    [SerializeField] private float damageRadius = 5f; // Радиус урона ударной волны
    [SerializeField] private PlayerStats playerStats;
    private void Update()
    {
    }
    public void ActivateCrimsonCarnage()
    {
        // Создаем ударную волну в указанной позиции
        playerStats.DecreaseUlt(playerStats.maxUlt.GetValue());
        // Наносим урон всем врагам в радиусе ударной волны
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                EnemyStats enemyStats = collider.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    // Применяем урон к врагу
                    enemyStats.TakeDamage(playerStats.damage.GetValue());
                }
            }
        }
    }
}
