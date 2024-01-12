using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMashine stateMashine;

    public D_Entity entityData;

    public int facingDirection { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGO { get; private set; }

    public AnimationToStatemashine atsm { get; private set; }
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;

    private float currentHealth;

    private int lastDamageDirection;

    private Vector2 velocityWokrSpace;

    public virtual void Start()
    {
        facingDirection = 1;
        currentHealth = entityData.maxHealth;

        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        atsm = aliveGO.GetComponent <AnimationToStatemashine>();
        
        stateMashine = new FiniteStateMashine();
    }

    public virtual void Update()
    {
        stateMashine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
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

        DamageHop(entityData.damageHopSpeed);

        if (attackDetails.position.x > aliveGO.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }
    }
    public virtual void SetVelosity(float velocity) 
    {
        velocityWokrSpace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWokrSpace;
    
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGrpund);
    }

    public virtual bool CheckLedge() 
    { 
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGrpund);
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);

    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position,aliveGO.transform.right,entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0.0f,180.0f,0.0f);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);


    }
}
