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
        currentHealth = enemyData.maxHealth;
        atsm = GetComponentInChildren<AnimationToStatemashine>();
    
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

    public virtual void DamageHop(float velocity)
    {
        velocityWokrSpace.Set(rb.velocity.x, velocity);
        rb.velocity = velocityWokrSpace;
    }
    public virtual void SetVelocityEnemy(float velocity)
    {
        velocityWokrSpace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWokrSpace;

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

    protected override void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * wallCheckDistance));
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + (Vector3)(Vector2.down * groundCheckDistance));
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * enemyData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * enemyData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * enemyData.maxAgroDistance), 0.2f);


    }
}
