using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrimsonCarnage : MonoBehaviour
{
    [SerializeField] private float damageRadius; // ������ ����� ������� �����
    [SerializeField] private float damage ; // ������ ����� ������� �����

    private float damageTimer = 0f;
    [SerializeField] private PlayerStats playerStats;
    private void Update()
    {
    }
    public void ActivateCrimsonCarnage()
    {
        // ������� ������� ����� � ��������� �������
        playerStats.DecreaseUlt(playerStats.maxUlt.GetValue());
        // ������� ���� ���� ������ � ������� ������� �����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                EnemyStats enemyStats = collider.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    // ��������� ���� � �����
                    enemyStats.TakeDamage(damage + damage * playerStats.magicAmplify.GetValue() / 100);
                }
            }
        }
    }
    public void BleedEffect(float bleedDamage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                EnemyStats enemyStats = collider.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    // ��������� ���� � �����
                    enemyStats.TakeDamage(bleedDamage);
                }
            }
        }
    }
}
