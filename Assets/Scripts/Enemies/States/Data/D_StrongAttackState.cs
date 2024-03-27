using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newStrongMeleeAttackStateData", menuName = "Data/State Data/Strong Melee Attack State")]

public class D_StrongAttackState : ScriptableObject
{
    public float attackcooldown = 3f;
    public float attackRadius = 0.8f;
    public float attackDamage = 10f;
    public LayerMask whatIsPlayer;
}
