using UnityEngine;

public class CelestialNexus : MonoBehaviour
{
    private PlayerStats playerStats;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveForce;
    [SerializeField] private float healthRestore; 
    [SerializeField] private float damage;
    [SerializeField] private float ticksPerSecond; // Частота тиков урона

    private float damageTimer = 0f;
    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        damageTimer += Time.deltaTime;
        if (damageTimer >= 1f / ticksPerSecond) // Проверяем, прошло ли достаточно времени для тика урона
        {
            damageTimer = 0f; // Сбрасываем таймер
            CheckCollisions();
        }
        MoveNexus();
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
    public void MoveNexus()
    {
        Player player = PlayerManager.instance.player;
        float attackRadius = playerStats.GetComponent<Entity>().GetAttackCheckRadius();

        // Проверяем, находится ли игрок в состоянии атаки и сфера в радиусе его атаки
        if (player.stateMachine.currentState == player.primaryAttackState &&
            Physics2D.OverlapCircle(transform.position, attackRadius, LayerMask.GetMask("Player")))
        {
            // Получаем направление, в котором смотрит игрок
            Vector2 playerDirection = player.transform.right; // Возможно, нужно использовать другое направление, зависит от ориентации вашего персонажа

            // Определяем расстояние между персонажем и сферой
            float distance = Vector2.Distance(player.transform.position, transform.position);

            // Уменьшаем силу в зависимости от расстояния
            float adjustedForce = moveForce / distance;

            // Применяем силу к сфере в направлении, в котором смотрит игрок
            rb.AddForce(playerDirection.normalized * adjustedForce, ForceMode2D.Impulse);
            Debug.Log("A DA");
        }
    }


}
