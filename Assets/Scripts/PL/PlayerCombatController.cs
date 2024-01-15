using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    //[SerializeField]
    //private bool combatEnabled;

    //private bool gotInput;

    //private float lastInoutTime;

    //private AttackDetails attackDetails;
    //private void Update()
    //{
    //    CheckCombatInput();
    //}
    //private void CheckCombatInput()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        if (combatEnabled)
    //        {
    //            gotInput = true;
    //            lastInoutTime = Time.time;
    //        }
    //    }
    //}

    //private void CheckAttaks()
    //{
    //    if (gotInput)
    //    {
    //        if (!isAttacking)
    //        {
    //            gotInput = false;
    //            anim.SetBool("attack1", true);
    //        }
    //    }
    //}
    //private void CheckAttackHitBox()
    //{
    //    Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackHitBox.position, attackRadius, whatIsDamageable);
    //    attackDetails.damageAmount = attack1Damage;
    //    attackDetails.position = transform.position;
    //    foreach (Collider2D collider in detectedObjects)
    //    {
    //        collider.transform.parent.SendMessage("Damage", attackDetails);
    //    }
    //}

    //private void FinishAttack1()
    //{
    //    isAttacking = false;
    //    anim.SetBool("attack1",false);
    //}
    
    //private void Damage(AttackDetails attackDetails)
    //{
    //    if(!PC.GetDashStatus())
    //    {
    //        int direction;
    //        PS.DecreaseHealht(attackDetails.damageAmount)
    //        if (attackDetails.position.x < transform.position.x)
    //        {
    //            direction = 1;
    //        }
    //        else
    //        {
    //            direction = -1;
    //        }
    //        PC.Knockback(direction);
    //    }
    //}

}
