using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class D_Enemy : ScriptableObject
{
    public float maxHealth = 30f;
    public float damageHopSpeed = 10.0f;
    public float enemyAttackDamage = 10f;
    public float maxAgroDistance = 4f;
    public float minAgroDistance = 2f;
    public float groundCheckRadius = 0.3f;

    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;

    public float closeRangeActionDistance = 1f;

    public GameObject hitParticle;

    public LayerMask whatIsPlayer;
}
