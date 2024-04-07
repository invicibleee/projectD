using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheSkillController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;

    private bool canRotate = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    public void SetupScythe(Vector2 _dir, float _gravityScale)
    {
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
    }

    private void Update()
    {
        if(canRotate)
            transform.right = rb.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canRotate = false;
        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        transform.parent = collision.transform;
    }
}
