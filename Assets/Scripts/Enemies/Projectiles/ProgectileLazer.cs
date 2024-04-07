using UnityEngine;

public class ProjectileLazer : MonoBehaviour
{
    private AttackDetails attackDetails;

    private float speed;
    private float initialScaleX; // Початковий розмір по осі x
    private float startTime;


    [SerializeField] 
    private float rotationSpeed;
    [SerializeField] 
    private float damageRadius;

    private bool hasHitGround;
    [SerializeField] 
    private LayerMask whatIsGround;
    [SerializeField] 
    private LayerMask whatIsPlayer;
    [SerializeField] 
    private Transform damagePosition;
    [SerializeField] 
    private bool destroyOnWallHit = true;

    private void Start()
    {
        initialScaleX = transform.localScale.x;
        attackDetails = new AttackDetails();
        startTime = Time.time; // Запам'ятовуємо час початку руху проектіля
    }

    private void Update()
    {
        if (!hasHitGround)
        {
            attackDetails.position = transform.position;

            // Rotate the object slightly around Z axis
            transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);

            // Scale the object along x-axis until it reaches damageRadius
            float elapsedTime = Time.time - startTime; // Визначаємо, скільки часу пройшло з моменту початку руху
            float scaleX = Mathf.Lerp(initialScaleX, damageRadius, elapsedTime * speed);
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);

            // Check if the object has reached the damage radius
            if (scaleX >= damageRadius)
            {
                Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
                Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

                if (damageHit)
                {
                    damageHit.transform.SendMessage("Damage", attackDetails);
                    Destroy(gameObject);
                }

                if (destroyOnWallHit && groundHit)
                {
                    hasHitGround = true;
                }
                else if (!destroyOnWallHit && groundHit)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void FireProjectile(float speed, float damage)
    {
        this.speed = speed;
        attackDetails.damageAmount = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
