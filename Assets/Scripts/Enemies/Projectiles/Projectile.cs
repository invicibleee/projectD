using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AttackDetails attackDetails;

    private float speed;
    private float travelDistance;
    private float xStartPosition;


    [SerializeField]
    private float maxGravity;

    [SerializeField]
    private float rotationSpeed; // Кутова швидкість обертання по осі Z

    [SerializeField]
    private float damageRadius;

    private Rigidbody2D rb;
    private bool hasHitGround;

    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private Transform damagePosition;

    [SerializeField]
    private bool destroyOnWallHit = true; // Whether to destroy on wall hit
    private bool isDamaged;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * speed;
        isDamaged = false;
        xStartPosition = transform.position.x;
    }

    private void Update()
    {
       //Debug.Log(isDamaged);
        if (!hasHitGround)
        {
            attackDetails.position = transform.position;

            // Calculate current gravity scale
            float currentGravity = Mathf.Lerp(0f, maxGravity, rb.velocity.magnitude / speed);

            // Set gravity scale
            rb.gravityScale = currentGravity;

            // Rotate the object slightly around Z axis
            transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
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
                PlayerStats target = damageHit.GetComponent<PlayerStats>();
                if (target != null)
                {
                    target.TakeDamage(attackDetails.damageAmount);
                    isDamaged = true;
                }

            }
            // Check for wall hit if destroyOnWallHit is enabled
            if (destroyOnWallHit)
            {
                if (groundHit)
                {
                    hasHitGround = true;
                    rb.gravityScale = 0f;
                    rb.velocity = Vector2.zero;
                }
            }
            else
            {
                if (groundHit)
                {
                    Destroy(gameObject);
                }
            }
        }
    }


    public void FireProjectile(float speed, float travelDistance, float damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        attackDetails.damageAmount = damage;
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
