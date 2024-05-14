using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newRangeTripleTargetAttackStateData", menuName = "Data/State Data/Range Triple Target Attack State")]
public class D_RangeTripleTargetAttackState : ScriptableObject
{
    public GameObject projectile;
    public float projectileDamage = 10.0f;
    public float projectileSpeed = 10.0f;
    public float projectileTravelDistance = 10.0f;
    public float pursuitTime = 2.0f;
    public float timeToDestroy = 3.0f;
    public float rangeTripleAttackCooldown = 6.0f;

    public int countOfShots = 3;
}
