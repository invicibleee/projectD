//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AegisAmuletCharm : Charm
//{
//   [SerializeField] private float damageReductionPercentage;
//    [SerializeField] private float damageReflectionPercentage; // ������� ����� ��� ���������
//    public override void CharmEffect()
//    {
//        playerStats.damageReceivedReductionPercentage += damageReductionPercentage;
//    }
//    public override void ActivateEffect()
//    {
//        base.ActivateEffect();
//        playerStats.OnDamageReceived += ReflectDamage;
//    }
//    public override void DeactivateEffect()
//    {
//        playerStats.damageReceivedReductionPercentage -= damageReductionPercentage;
//        base.DeactivateEffect();
//        playerStats.OnDamageReceived -= ReflectDamage;
//    }

//    private void ReflectDamage(float damage)
//    {
//        // ��������� ���������� ����� ��� ���������
//        float reflectedDamage = damage * (damageReflectionPercentage / 100f);
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AegisAmuletCharm : Charm
{
    [SerializeField] private float damageReductionPercentage;
    [SerializeField] private float damageReflectionPercentage; // ������� ����� ��� ���������

    private CharacterStats enemyInflictingDamage; // ���������� ��� �������� �����, ���������� ����

    public override void CharmEffect()
    {
        playerStats.damageReceivedReductionPercentage += damageReductionPercentage;
    }

    public override void ActivateEffect()
    {
        base.ActivateEffect();
        playerStats.OnDamageReceived += ReflectDamageAndEnemy;
    }

    public override void DeactivateEffect()
    {
        playerStats.damageReceivedReductionPercentage -= damageReductionPercentage;
        base.DeactivateEffect();
        playerStats.OnDamageReceived -= ReflectDamageAndEnemy;
    }

    private void ReflectDamageAndEnemy(float damage)
    {
        // ��������� �����, ������� ������� ����
        enemyInflictingDamage = GetNearestEnemy();

        // ��������� ���������� ����� ��� ���������
        float reflectedDamage = damage * (damageReflectionPercentage / 100f);

        // ��������� ������ ����� �� �����
        if (enemyInflictingDamage != null)
        {
            enemyInflictingDamage.TakeDamage(reflectedDamage);
        }
    }

    private CharacterStats GetNearestEnemy()
    {
        // ������� ���������� �����, ������������� �� ����� ������� �����
        Collider2D hit = Physics2D.OverlapCircle(playerStats.transform.position, 10f, LayerMask.GetMask("Enemy"));

        if (hit != null && hit.GetComponent<EnemyStats>() != null)
        {
            Debug.Log("Found nearest enemy: " + hit.gameObject.name); // ������� ��� ���������� ����� � �������
            return hit.GetComponent<EnemyStats>();
        }

        Debug.Log("No nearest enemy found!"); // ������� ���������, ���� ���� �� ������
        return null; // ���� �� ���� ���� �� ������
    }

}

