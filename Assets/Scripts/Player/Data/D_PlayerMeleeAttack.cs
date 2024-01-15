using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newPlayerMeleeAttackData", menuName = "Data/PlayerState Data/Player Melee Attack Data")]
public class D_PlayerMeleeAttack : ScriptableObject
{
    public float attackRadius = 1f;
    public float attackDamage = 10f;
    public LayerMask whatIsEnemy;
}
