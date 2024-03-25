using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newRangeAttackStateData", menuName = "Data/State Data/Range Attack State")]

public class D_RangeAttackState : ScriptableObject
{
    public GameObject projectile;
    public float projectileDamage = 10.0f;
    public float projectileSpeed = 10.0f;
    public float projectileTravelDistance = 10.0f;
}
