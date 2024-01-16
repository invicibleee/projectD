using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class D_Enemy : ScriptableObject
{
    public float maxHealth = 30f;

    public float damageHopSpeed = 10.0f;

    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    
    public float maxAgroDistance = 4f;
    public float minAgroDistance = 2f;

    public float closeRangeActionDistance = 1f;

    public LayerMask whatIsPlayer;
    public LayerMask whatIsGrpund;
}
