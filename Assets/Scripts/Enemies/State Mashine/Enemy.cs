using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyStateMashine stateMashine;

    public D_Enemy enemyData;
    public AnimationToStatemashine atsm { get; private set; }
    public int lastDamageDirection { get; private set; }


    [SerializeField]
    private Transform playerCheck;
    [SerializeField] 
    private Transform groundCheckStun;
    private float currentHealth;
    private float currentStunResistence =0;
    private float lastDamageTime;

    private Vector2 velocityWokrSpace;

    protected bool isStunned;
    protected bool isDead;
    protected override void Start()
    {
        base.Start();
        currentHealth = enemyData.maxHealth;
        currentStunResistence = enemyData.stunResistance;
        
        atsm = GetComponentInChildren<AnimationToStatemashine>();
    
        stateMashine = new EnemyStateMashine();
    }

    protected override void Update()
    {
        base.Update();
        stateMashine.currentState.LogicUpdate();
        if (Time.time >= lastDamageTime + enemyData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        stateMashine.currentState.PhysicsUpdate();
    }

    public virtual void DamageHop(float velosity)
    {
        velocityWokrSpace.Set(rb.velocity.x, velosity);
        rb.velocity = velocityWokrSpace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistence = enemyData.stunResistance;
    }

    //public virtual void Damage(AttackDetails attackDetails)
    //{
    //    lastDamageTime = Time.time;
    //    currentHealth -= attackDetails.damageAmount;
    //    currentStunResistence -= attackDetails.stunDamageAmount;
    //    DamageHop(enemyData.damageHopSpeed);

    //    if (attackDetails.position.x > rb.transform.position.x)
    //    {
    //        lastDamageDirection = -1;
    //    }
    //    else
    //    {
    //        lastDamageDirection = 1;
    //    }

    //    if(currentStunResistence <= 0)
    //    {
    //        isStunned= true;
    //    }
    //}
    public override void Damage()
    {
        base.Damage();
        currentHealth = enemyData.maxHealth;
        
        Instantiate(enemyData.hitParticle,rb.transform.position, Quaternion.Euler(0f,0f,Random.Range(0f,360f)));
        if(currentHealth <= 0)
        {
            isDead = true;
        }
    }
    public virtual void SetVelocityEnemy(float velocity)
    {
        velocityWokrSpace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWokrSpace;

    }
    public virtual void SetVelocityEnemy(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWokrSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = velocityWokrSpace;
    }
    public virtual bool CheckGourndAround()
    {
        return Physics2D.OverlapCircle(groundCheckStun.position, enemyData.groundCheckRadius, whatIsGround);
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, rb.transform.right, enemyData.minAgroDistance, enemyData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, rb.transform.right, enemyData.maxAgroDistance, enemyData.whatIsPlayer);

    }
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position,rb.transform.right,enemyData.closeRangeActionDistance, enemyData.whatIsPlayer);
    }

    public override void Flip()
    {
        base.Flip();
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * wallCheckDistance));
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + (Vector3)(Vector2.down * groundCheckDistance));
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * enemyData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * enemyData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * enemyData.maxAgroDistance), 0.2f);


    }
}
