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

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;

    private Vector2 velocityWokrSpace;

    public virtual void Start()
    {
        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        
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

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0.0f,180.0f,0.0f);
    }


}
