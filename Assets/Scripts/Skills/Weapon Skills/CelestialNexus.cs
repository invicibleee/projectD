using UnityEngine;

public class CelestialNexus : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats; 
    [SerializeField] private float healthRestore; 
    [SerializeField] private float damage;
    [SerializeField] private float ticksPerSecond; // Частота тиков урона

    private float damageTimer = 0f;
    private void Start()
    {
    }
    private void Update()
    {
        damageTimer += Time.deltaTime;
        if (damageTimer >= 1f / ticksPerSecond) // Проверяем, прошло ли достаточно времени для тика урона
        {
            damageTimer = 0f; // Сбрасываем таймер
            CheckCollisions();
        }
    }
    private void CheckCollisions()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                playerStats.IncreaseHealthBy(healthRestore);
            }
            else if (collider.CompareTag("Enemy"))
            {
                EnemyStats enemyStats = collider.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    enemyStats.TakeDamageWithoutKnockback(damage);
                }
            }
        }
    }
}
