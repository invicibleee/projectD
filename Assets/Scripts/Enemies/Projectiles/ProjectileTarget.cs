using UnityEngine;

public class ProjectileTarget : MonoBehaviour
{
    private AttackDetails attackDetails;
    private Player targetPlayer;
    private float speed;
    private float travelDistance;
    private float xStartPosition;
    private float pursuitTime; // Час переслідування
    private float currentPursuitTime; // Поточний час переслідування
    private bool isPursuing; // Позначка переслідування

    [SerializeField]
    private float gravity;
    [SerializeField]
    private float damageRadius;

    private Rigidbody2D rb;
    private bool isGravitiOn;
    private bool hasHitGround;

    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private Transform damagePosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPlayer = Transform.FindAnyObjectByType<Player>();
        rb.gravityScale = 0.0f;

        isGravitiOn = false;
        isPursuing = true; // Початково об'єкт переслідує гравця
        currentPursuitTime = 0f; // Початковий час переслідування

        xStartPosition = transform.position.x;
    }

    private void Update()
    {
        if (!hasHitGround)
        {
            if (isPursuing)
            {
                if (targetPlayer != null)
                {
                    Vector2 direction = (targetPlayer.transform.position - transform.position).normalized;
                    rb.velocity = direction * speed;
                }

                // Якщо час переслідування вичерпано, зупиняємо переслідування
                if (currentPursuitTime >= pursuitTime)
                {
                    isPursuing = false;
                }
                else
                {
                    currentPursuitTime += Time.deltaTime;
                }
            }
            else
            {
                // Якщо переслідування завершено, об'єкт рухається прямо вперед
                rb.velocity = transform.right * speed;
            }

            if (isGravitiOn)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            if (damageHit)
            {
                damageHit.transform.SendMessage("Damage", attackDetails);
                Destroy(gameObject);
            }


            if (Mathf.Abs(xStartPosition - transform.position.x) >= travelDistance && !isGravitiOn)
            {
                isGravitiOn = true;
                rb.gravityScale = gravity;
            }
        }
    }

    public void FireProjectile(float speed, float travelDistance, float damage, float pursuitTime)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        attackDetails.damageAmount = damage;
        this.pursuitTime = pursuitTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
