using UnityEngine;

public class ProjectileTarget : MonoBehaviour
{
    private AttackDetails attackDetails;
    private Player targetPlayer;
    
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float damageRadius;
    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private Transform damagePosition;

    private float speed;
    private float travelDistance;
    private float xStartPosition;

    private float pursuitTime;
    private float currentPursuitTime;
    private float startTime;
    private float timeToDestroy;

    private Rigidbody2D rb;
    
    private bool isGravitiOn;
    private bool hasHitGround;
    private bool isPursuing; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPlayer = Transform.FindAnyObjectByType<Player>();
        rb.gravityScale = 0.0f;
        startTime = Time.time;
        isGravitiOn = false;
        isPursuing = true; 
        currentPursuitTime = 0f; 
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
                rb.velocity = transform.right * speed;
            }

            if (isGravitiOn)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            if (Time.time >= startTime + timeToDestroy)
            {
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);

            if (damageHit)
            {
                PlayerStats target = damageHit.GetComponent<PlayerStats>();
                if (target != null)
                {
                    target.TakeDamage(attackDetails.damageAmount);
                }

            }

            if (Mathf.Abs(xStartPosition - transform.position.x) >= travelDistance && !isGravitiOn)
            {
                isGravitiOn = true;
                rb.gravityScale = gravity;
            }
        }
    }

    public void FireProjectile(float speed, float travelDistance, float damage, float pursuitTime,float timeToDestroy)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        attackDetails.damageAmount = damage;
        this.pursuitTime = pursuitTime;
        this.timeToDestroy = timeToDestroy;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
