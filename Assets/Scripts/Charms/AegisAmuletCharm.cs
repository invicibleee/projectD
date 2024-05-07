//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AegisAmuletCharm : Charm
//{
//   [SerializeField] private float damageReductionPercentage;
//    [SerializeField] private float damageReflectionPercentage; // Процент урона для отражения
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
//        // Вычислите количество урона для отражения
//        float reflectedDamage = damage * (damageReflectionPercentage / 100f);
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AegisAmuletCharm : Charm
{
    [SerializeField] private float damageReductionPercentage;
    [SerializeField] private float damageReflectionPercentage; // Процент урона для отражения

    private CharacterStats enemyInflictingDamage; // Переменная для хранения врага, наносящего урон

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
        // Получение врага, который наносит урон
        enemyInflictingDamage = GetNearestEnemy();

        // Вычислить количество урона для отражения
        float reflectedDamage = damage * (damageReflectionPercentage / 100f);

        // Применить эффект урона на врага
        if (enemyInflictingDamage != null)
        {
            enemyInflictingDamage.TakeDamage(reflectedDamage);
        }
    }

    private CharacterStats GetNearestEnemy()
    {
        // Находим ближайшего врага, определяемого по месту события урона
        Collider2D hit = Physics2D.OverlapCircle(playerStats.transform.position, 10f, LayerMask.GetMask("Enemy"));

        if (hit != null && hit.GetComponent<EnemyStats>() != null)
        {
            Debug.Log("Found nearest enemy: " + hit.gameObject.name); // Выводим имя найденного врага в консоль
            return hit.GetComponent<EnemyStats>();
        }

        Debug.Log("No nearest enemy found!"); // Выводим сообщение, если враг не найден
        return null; // Если ни один враг не найден
    }

}

