using UnityEngine;

public class CelestialNexus : MonoBehaviour
{
    private PlayerStats playerStats;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveForce;
    [SerializeField] private float healthRestore; 
    [SerializeField] private float damage;
    [SerializeField] private float ticksPerSecond; // ������� ����� �����

    private float damageTimer = 0f;
    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        damageTimer += Time.deltaTime;
        if (damageTimer >= 1f / ticksPerSecond) // ���������, ������ �� ���������� ������� ��� ���� �����
        {
            damageTimer = 0f; // ���������� ������
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

        // ���������, ��������� �� ����� � ��������� ����� � ����� � ������� ��� �����
        if (player.stateMachine.currentState == player.primaryAttackState &&
            Physics2D.OverlapCircle(transform.position, attackRadius, LayerMask.GetMask("Player")))
        {
            // �������� �����������, � ������� ������� �����
            Vector2 playerDirection = player.transform.right; // ��������, ����� ������������ ������ �����������, ������� �� ���������� ������ ���������

            // ���������� ���������� ����� ���������� � ������
            float distance = Vector2.Distance(player.transform.position, transform.position);

            // ��������� ���� � ����������� �� ����������
            float adjustedForce = moveForce / distance;

            // ��������� ���� � ����� � �����������, � ������� ������� �����
            rb.AddForce(playerDirection.normalized * adjustedForce, ForceMode2D.Impulse);
            Debug.Log("A DA");
        }
    }


}
