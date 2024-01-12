using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newMeleeAttackData", menuName = "Data/State Data/Melee Attack Data")]
public class D_MeleeAttack : ScriptableObject
{
    public float attackRadius = 1f;
    public float attackDamage = 10f;
    public LayerMask whatIsPlayer;
}
