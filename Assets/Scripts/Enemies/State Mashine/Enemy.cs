using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public FiniteStateMashine stateMashine;

    public D_Enemy enemyData;
    public AnimationToStatemashine atsm { get; private set; }



    [SerializeField]
    private Transform playerCheck;

    private float currentHealth;

    private int lastDamageDirection;

    private Vector2 velocityWokrSpace;

    protected override void Start()
    {
        base.Start();


        atsm = GetComponentInChildren<AnimationToStatemashine>();

        currentHealth = enemyData.maxHealth;

        
        stateMashine = new FiniteStateMashine();
    }

    protected override void Update()
    {
        base.Update();
        stateMashine.currentState.LogicUpdate();
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

    public virtual void Damage(AttackDetails attackDetails)
    {
        currentHealth -= attackDetails.damageAmount;

        DamageHop(enemyData.damageHopSpeed);

        if (attackDetails.position.x > rb.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }
    }
    public virtual void SetVelocityEnemy(float velocity)
    {
        velocityWokrSpace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWokrSpace;

    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, rb.transform.right, wallCheckDistance, whatIsGround);
    }

    public virtual bool CheckLedge() 
    { 
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
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

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * wallCheckDistance));
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + (Vector3)(Vector2.down * groundCheckDistance));
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * enemyData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * enemyData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * enemyData.maxAgroDistance), 0.2f);


    }
}
