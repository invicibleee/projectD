using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Collision info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;

    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    public SkillManager skill { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public CharacterStats stats { get; private set; }


    #endregion

    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;



    public int facingDirection { get; private set; } = 1;
    public bool facingRight = true;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
        stats = GetComponent<CharacterStats>();

        skill = SkillManager.instance;
    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {

    }

    public virtual void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {

    }

    protected virtual void ReturnDefaultSpeed()
    {
        anim.speed = 1;
    }
    public virtual void DamageEffect()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
        
    }
    public virtual void DamageImpact() => StartCoroutine("HitKnockback");
    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDirection.x * -facingDirection, knockbackDirection.y);

        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }

    #region Velocity
    public virtual void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnocked)
            return;

        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    public void ZeroVelocity() 
    {
        if (isKnocked)
            return;

        rb.velocity = new Vector2(0, 0);
    }
    #endregion
    #region Flip
    public virtual void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    protected virtual void FlipController(float x)
    {
        if (x > 0 && !facingRight)
            Flip();
        if (x < 0 && facingRight)
            Flip();
    }
    #endregion
    #region Collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y, groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x, wallCheck.position.y, wallCheckDistance));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion
    public void SetAttackCheckRadius(float radius)
    {
        attackCheckRadius = radius;
    }
    public float GetAttackCheckRadius()
    {
        return attackCheckRadius;
    }
    public void MakeTransparent(bool _transparent)
    {
        Debug.Log(_transparent);
        if (_transparent)
            sr.color = Color.clear;
        else
            sr.color = Color.white;
    }
    public virtual void Die()
    {

    }
}
