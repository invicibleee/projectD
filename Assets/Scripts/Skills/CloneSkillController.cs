using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private float colorLoosingSpeed;

    private float cloneTimer;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        cloneTimer -= Time.deltaTime;

        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLoosingSpeed));
        }
    }

    public void SetupClone(Transform newTransform, float cloneDuration, bool canAttack)
    {
        if (canAttack)
        {
            anim.SetInteger("AttackNumber", Random.RandomRange(1, 3));
        }
        transform.position = newTransform.position;
        cloneTimer = cloneDuration;
    }

    private void AnimationTrigger()
    {

    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach(var hit in colliders)
        {

        }
    }
}
