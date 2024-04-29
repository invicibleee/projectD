using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatonTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
    
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach(var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
               EnemyStats _target = hit.GetComponent<EnemyStats>();

                player.stats.DoDamage(_target);
            }
                
        }
    }

    private void ThrowScythe()
    {
        SkillManager.instance.scytheThrow.CreateScythe();
    }
}
